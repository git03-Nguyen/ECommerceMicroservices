using Newtonsoft.Json;

namespace Customer.Service.Validation;

public class ValidationResultModel
{
    public ValidationResultModel()
    {
        Errors = new List<Customer.Service.Validation.ValidationError>();
    }

    public ValidationResultModel(List<Customer.Service.Validation.ValidationError> errors)
    {
        Errors = errors;
    }

    public List<Customer.Service.Validation.ValidationError> Errors { get; }

    public bool IsValid => !Errors.Any();

    public void AddError(string field, string errorMessage, string errorMessageCode)
    {
        Errors.Add(new Customer.Service.Validation.ValidationError(field, errorMessage, errorMessageCode));
    }

    public void AddError(Customer.Service.Validation.ValidationError error)
    {
        Errors.Add(error);
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}