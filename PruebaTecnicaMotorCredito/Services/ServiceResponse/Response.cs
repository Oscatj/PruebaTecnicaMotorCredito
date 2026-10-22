namespace PruebaTecnicaMotorCredito.Services.ServiceResponse
{
    public class Response<T>
    {
        public Response(string message, int stutusCode, T data)
        {
            Message = message;
            StutusCode = stutusCode;
            Data = data;
        }

        public string Message { get; set; }
        public int StutusCode { get; set; }
        public T Data { get; set; }
    }
}
