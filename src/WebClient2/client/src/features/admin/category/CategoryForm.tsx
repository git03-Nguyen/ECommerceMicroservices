import { Box, Paper, Typography, Grid, Button } from "@mui/material";
import { useEffect } from "react";
import { FieldValues, useForm } from "react-hook-form";
import AppTextInput from "../../../app/components/AppTextInput";
import { yupResolver } from "@hookform/resolvers/yup";
import { validationSchema } from "./CategoryValidation";
import agent from "../../../app/api/agent";
import { useAppDispatch } from "../../../app/store/configureStore";
import { setCategory } from "../../catalog/catalogSlice";
import { LoadingButton } from "@mui/lab";

interface Props {
  category?: { name: string; description: string };
  cancelEdit: () => void;
}

export default function CategoryForm({ category, cancelEdit }: Props) {
  const {
    control,
    reset,
    handleSubmit,
    formState: { isDirty, isSubmitting },
  } = useForm({
    resolver: yupResolver(validationSchema),
  });

  const dispatch = useAppDispatch();

  useEffect(() => {
    if (category && !isDirty) reset(category);
  }, [category, reset, isDirty]);

  async function handleSubmitData(data: FieldValues) {
    try {
      let response;
      if (category) {
        // Assuming category has an id property
        response = await agent.Admin.updateCategory(data);
      } else {
        response = await agent.Admin.createCategory(data);
      }
      dispatch(setCategory(response.payload));
      cancelEdit();
    } catch (error) {
      console.log(error);
    }
  }

  return (
    <Box component={Paper} sx={{ p: 4 }}>
      {category ? (<Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
        Edit Category
      </Typography>) : (
        <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
          Add Category
        </Typography>
      )}
      <form onSubmit={handleSubmit(handleSubmitData)}>
        <Grid container spacing={3}>
          <Grid item xs={12}>
            <AppTextInput control={control} name="name" label="Category name" />
          </Grid>
          <Grid item xs={12}>
            <AppTextInput
              control={control}
              multiline={true}
              rows={4}
              name="description"
              label="Description"
            />
          </Grid>
        </Grid>
        <Box display="flex" justifyContent="space-between" sx={{ mt: 3 }}>
          <Button onClick={cancelEdit} variant="contained" color="inherit">
            Cancel
          </Button>
          <LoadingButton
            loading={isSubmitting}
            type="submit"
            variant="contained"
            color="success"
          >
            Submit
          </LoadingButton>
        </Box>
      </form>
    </Box>
  );
}
