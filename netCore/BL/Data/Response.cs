using IBL.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Data
{
    public class Response<TData> : IResponse<TData>
    {

        StringBuilder _messages = new StringBuilder();
        public Response(ActionStatus status)
        {
            Status = status;
        }
        public Response(ActionStatus status, TData data) : this(status)
        {
            Data = data;
        }
        public Response(ActionStatus status, TData data,string message) : this(status,data)
        {
            _messages.AppendLine(message);
        }
        public Response(ActionStatus status, TData data, IEnumerable<string> messages) : this(status, data)
        {
            foreach (string m in messages)
            {
                _messages.AppendLine(m);
            }
        }
        public void AddMessage(string message)
        {
            _messages.AppendLine(message);
        }
        public TData Data { get; private set; }

        public ActionStatus Status { get; private set; }

        public string Message { get { return _messages.ToString(); } }
    }
}
