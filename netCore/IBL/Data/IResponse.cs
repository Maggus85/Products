using System;
using System.Collections.Generic;
using System.Text;

namespace IBL.Data
{
    public interface IResponse<TData>
    {
        TData Data { get; }
        ActionStatus Status { get; }
        string Message { get; }
    }
}
