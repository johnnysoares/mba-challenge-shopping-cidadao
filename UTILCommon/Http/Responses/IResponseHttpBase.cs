namespace UTILCommon.Http.Responses {

    public interface IResponseHttpBase {
        
        string status { get; set; }
        
        string mensagem { get; set; }
    }

}