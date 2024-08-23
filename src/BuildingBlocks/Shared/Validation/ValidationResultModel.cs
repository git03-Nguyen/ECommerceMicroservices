using Newtonsoft.Json;

namespace Shared.Validation;

public class ValidationResultModel
{
    public List<ValidationError> Errors { get; }

    public ValidationResultModel()
    {
        Errors = new List<ValidationError>();
    }

    public ValidationResultModel(List<ValidationError> errors)
    {
        Errors = errors;
    }

    public void AddError(string field, string errorMessage, string errorMessageCode)
    {
        Errors.Add(new ValidationError(field, errorMessage, errorMessageCode));
    }

    public void AddError(ValidationError error)
    {
        Errors.Add(error);
    }

    public bool IsValid => !Errors.Any();

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
    
}