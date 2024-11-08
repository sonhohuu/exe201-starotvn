﻿namespace Exe.Starot.Api.Controllers
{
    public class JsonResponse<T>
    {
        private object value;

        public JsonResponse(object value)
        {
            this.value = value;
        }

        public JsonResponse(int status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        
    }
}
