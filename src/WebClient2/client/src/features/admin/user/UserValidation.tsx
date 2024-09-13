import * as Yup from 'yup';

// Define the validation schema for the user form
export const validationSchema = Yup.object({
  userName: Yup.string().required('Name is required'),
  email: Yup.string().email('Invalid email').required('Email is required'),
  // password: Yup.string().min(6, 'Password must be at least 6 characters'),
  role: Yup.string().required('Role is required'),
  fullName: Yup.string().required('Full Name is required'),
  phoneNumber: Yup.string().required('Phone Number is required').matches(/^[0-9]+$/, 'Phone Number must contain only digits'),
  address: Yup.string().required('Address is required'),
});
