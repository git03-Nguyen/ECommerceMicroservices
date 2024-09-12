import * as yup from "yup";

export const validationSchema = yup.object({
  categoryName: yup.string().required(),
  name: yup.string().required(),
  price: yup.number().required().moreThan(1),
  stock: yup.number().required().min(0),
  description: yup.string().required(),
  // imageUrl: yup.mixed().when("imageUrl", {
  //   is: (value: string) => {
  //     return !value;
  //   }, //when image.productUrl is undefined
  //   then: (schema) => schema.required("Please provide an image"),
  //   otherwise: (schema) => schema.notRequired(),
  // }),
});
