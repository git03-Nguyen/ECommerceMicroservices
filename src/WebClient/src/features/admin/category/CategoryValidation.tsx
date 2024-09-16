import * as Yup from 'yup';

// Define the validation schema for the category form
export const validationSchema = Yup.object({
  name: Yup.string().required('Name is required'),
  description: Yup.string().required('Description is required'),
});
