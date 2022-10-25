using Jzo.IncrowdSports.Shared.Messages;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Jzo.IncrowdSports.Shared
{

    /// <summary>Represents the message formatter to send and receive message from a stream</summary>
    /// <typeparam name="TMessage">The type of the message.</typeparam>
    public class MessageFormatter<TMessage> where TMessage : MessageBase, new()
    {

        public static readonly int MAX_MESSAGE_HEADER_LENGTH = 5;

        private readonly JsonSerializerOptions mSerializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private readonly JsonSerializerOptions mDeserializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        /// <summary>
        /// Reads content and deserialize it from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The T object</returns>
        public TMessage? Read(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            int b = stream.ReadByte();
            if (b == 0)
            {
                b = stream.ReadByte(); // skip marker byte
            }
            if (b == 0)
            {
                throw new FormatException("Invalid message length.");
            }

            TMessage? result = default(TMessage);

            using (MemoryStream ms = new MemoryStream())
            {
                int messageLength = 0;
                while (b > 0)
                {
                    ms.WriteByte((byte)b);
                    if (ms.Length > MAX_MESSAGE_HEADER_LENGTH)
                    {
                        throw new FormatException("Message header too long.");
                    }
                    b = stream.ReadByte();
                }
                if (!int.TryParse(Encoding.UTF8.GetString(ms.ToArray()), out messageLength))
                {
                    throw new FormatException("Invalid message header length value.");
                }
                ms.SetLength(0);

                // reading the expected data length
                byte[] buffer = new byte[messageLength];
                int readBytes = 0;
                while (readBytes < buffer.Length)
                {
                    readBytes += stream.Read(buffer, readBytes, buffer.Length - readBytes);
                }

                ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;
                result = JsonSerializer.Deserialize<TMessage>(ms);
                ms.SetLength(0);
            }

            return result;
        }

        /// <summary>
        /// Writes data into the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="data">The data.</param>
        public void Write(Stream stream, TMessage data)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (data == null) throw new ArgumentNullException("data");

            using (MemoryStream dataStream = new MemoryStream())
            {
                JsonSerializer.Serialize(dataStream, data);
                using (MemoryStream headerStream = new MemoryStream())
                {
                    byte[] lenBytes = Encoding.UTF8.GetBytes(dataStream.Length.ToString());
                    headerStream.WriteByte((byte)0); // marker byte
                    headerStream.Write(lenBytes, 0, lenBytes.Length); // length of the message
                    headerStream.WriteByte((byte)0); // marker byte
                    headerStream.Position = 0;
                    headerStream.CopyTo(stream); // writing header
                    dataStream.Position = 0;
                    dataStream.CopyTo(stream); // writing data
                }
            }
        }

    }

}