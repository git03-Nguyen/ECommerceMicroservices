import * as yup from "yup";

export const validationSchema = yup.object({
  categoryName: yup.string().required('Category Name is required'),
  name: yup.string().required('Product Name is required'),
  price: yup.number().required('Price is required').moreThan(0, 'Price must be greater than 0'),
  stock: yup.number().required('Stock is required').moreThan(0, 'Stock must be greater than 0').integer('Stock must be an integer'),
  description: yup.string().required('Description is required'),

  imageUrl: yup.string()
    .nullable()
    .test('is-valid-url', 'ImageUrl is not a valid URL', value => {
      return !value || value.startsWith('http') || value.startsWith('/images'); // Allow empty string or URLs starting with http:// or /images
    }),

  imageUpload: yup.mixed()
    .nullable(),

}).test(
  'imageUrl-or-imageUpload',
  'Either ImageUrl or ImageUpload is required',
  function (value) {
    return !!value.imageUrl || !!value.imageUpload; // Ensure that at least one is provided
  }
).test(
  'not-both',
  'You cannot provide both ImageUrl and ImageUpload',
  function (value) {
    return !(value.imageUrl && value.imageUpload); // Ensure that both are not provided at the same time
  }
);
