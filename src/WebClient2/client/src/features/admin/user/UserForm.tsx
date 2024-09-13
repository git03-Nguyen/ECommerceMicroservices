import { Box, Paper, Typography, Grid, Button } from "@mui/material";
import { useEffect } from "react";
import { FieldValues, set, useForm } from "react-hook-form";
import AppTextInput from "../../../app/components/AppTextInput";
import { yupResolver } from "@hookform/resolvers/yup";
import { validationSchema } from "./UserValidation";
import agent from "../../../app/api/agent";
import { useAppDispatch } from "../../../app/store/configureStore";
import { LoadingButton } from "@mui/lab";
import { User } from "../../../app/models/user";
import AppSelectList from "../../../app/components/AppSelectList";
import AppDropzone from "../../../app/components/AppDropzone";
import { router } from "../../../app/router/routes";
import { toast } from "react-toastify";

interface Props {
  user?: User | undefined;
  cancelEdit: () => void;
}

const catalogUrl = process.env.REACT_APP_CATALOG_URL!;

export default function UserForm({ user, cancelEdit }: Props) {
  const {
    control,
    reset,
    handleSubmit,
    watch,
    formState: { isDirty, isSubmitting },
  } = useForm({
    resolver: yupResolver(validationSchema),
  });

  const watchImage = watch("pictureUrl", null);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (user && !watchImage && !isDirty) reset(user);
    return () => {
      if (watchImage) URL.revokeObjectURL(watchImage.preview);
    };
  }, [user, reset, watchImage, isDirty]);

  async function handleSubmitData(data: FieldValues) {
    try {
      let response;
      if (user) {
        // Assuming category has an id property
        response = await agent.Admin.updateUser(data);
        toast.success("Update user successfully");
      } else {
        response = await agent.Admin.createUser(data);
        toast.success("Create user successfully");
      }
      cancelEdit();

      // rerender the user list 
      // router.navigate("/user-management");
      // 

      // dispatch(setCategory(response.payload));
    } catch (error: any) {
      console.log(error);
      toast.error(`Error: ${error.data?.message ?? "Unknown error"}`)
    }
  }

  return (
    <Box component={Paper} sx={{ p: 4 }}>
      <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
        User Details
      </Typography>
      <form onSubmit={handleSubmit(handleSubmitData)}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={12}>
            <AppTextInput control={control} name="userName" label="User name" />
          </Grid>
          <Grid item xs={12} sm={6}>
            <AppSelectList
              control={control}
              items={["Admin", "Seller", "Customer"]}
              name="role"
              label="Role"
            />
          </Grid>
          <Grid item xs={12} sm={6}>
            <AppTextInput
              type="email"
              control={control}
              name="email"
              label="Email"
            />
          </Grid>
          {/* <Grid item xs={12} sm={6}>
            <AppTextInput
              type="password"
              control={control}
              name="password"
              label="Password"
            />
          </Grid> */}
          <Grid item xs={12}>
            <AppTextInput
              control={control}
              name="fullName"
              label="Full Name"
            />
          </Grid>
          <Grid item xs={12} sm={6}>
            <AppTextInput
              control={control}
              name="phoneNumber"
              label="Phone Number"
            />
          </Grid>
          <Grid item xs={12} sm={6}>
            <AppTextInput
              control={control}
              name="address"
              label="Address"
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <AppTextInput control={control} name="avatar" label="Avatar URL" />
          </Grid>
          <Grid item xs={12}>
            <Box
              display="flex"
              justifyContent="space-between"
              alignItems="center"
            >
              <AppDropzone control={control} name="avatarUpload" />

              {watchImage ? (
                <img
                  src={watchImage.preview}
                  alt="preview"
                  style={{ maxHeight: 200 }}
                />
              ) : (
                <img
                  src={
                    !user ?
                      "https://placehold.co/600x400?text=Preview+Image" :
                      user?.avatar?.startsWith("http") ?
                        user?.avatar :
                        `${catalogUrl}${user?.avatar}`
                  }
                  alt={user?.fullName ?? "preview"}
                  style={{
                    maxHeight: 200
                  }}
                />
              )}
            </Box>
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
