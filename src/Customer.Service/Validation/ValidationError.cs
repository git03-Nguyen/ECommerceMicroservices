namespace Customer.Service.Validation;

public class ValidationError
{

    public ValidationError(string field, string errorMessage, string errorMessageCode)
    {
        Field = field != string.Empty ? field : null;
        ErrorMessage = errorMessage;
        ErrorMessageCode = errorMessageCode;
    }

    public string Field { get; set; }
    public string ErrorMessage { get; set; }
    public string ErrorMessageCode { get; set; }
}