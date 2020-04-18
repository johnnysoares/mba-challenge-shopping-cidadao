namespace UTILCommon.Http.Responses {

    public class ResponseHttpBase : IResponseHttpBase {

        public virtual int code { get; set; }
        
        public virtual string status { get; set; }

        public virtual string mensagem { get; set; }
    }

}
