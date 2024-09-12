import * as yup from "yup";

export const validationSchema = [
  yup.object({
    fullName: yup.string().required("Full name is required"),
    address: yup.string().required("Address line is required"),
    phoneNumber: yup.string().required("Phone number is required"),
  }),
  yup.object(),
  yup.object({
    nameOnCard: yup.string().required("Name on Card is required"),
  }),
];
