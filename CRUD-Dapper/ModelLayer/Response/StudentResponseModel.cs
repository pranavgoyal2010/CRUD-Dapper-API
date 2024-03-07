namespace ModelLayer.Responses;

public class StudentResponseModel<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; }
    //public int StatusCode { get; set; }
    public T? Data { get; set; }
    //public List<StudentEntity> Data { get; set; }
}
