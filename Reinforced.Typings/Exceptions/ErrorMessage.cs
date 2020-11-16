using System;
using System.Linq;

namespace Reinforced.Typings.Exceptions
{
    class ErrorMessage
    {
        public int Code { get; private set; }

        public string MessageText { get; private set; }

        public string Subcategory { get; private set; }

        public ErrorMessage(int code, string messageText, string subcategory = "")
        {
            Code = code;
            MessageText = messageText;
            Subcategory = subcategory;
        }

        public void Throw(params object[] formatParameters)
        {
            Exception innerException = formatParameters?.OfType<Exception>().FirstOrDefault();
            object[] parameters = innerException is null ? formatParameters : 
                formatParameters?.Where((param) => !ReferenceEquals(param, innerException)).ToArray();

            parameters ??= Array.Empty<object>();
            
            throw new RtException(string.Format(MessageText, parameters), Code, Subcategory, innerException);
        }

        public RtWarning Warn(params object[] formatParameters)
        {
            return new RtWarning(Code, text: string.Format(MessageText, formatParameters), subcategory: Subcategory);
        }
    }
}
