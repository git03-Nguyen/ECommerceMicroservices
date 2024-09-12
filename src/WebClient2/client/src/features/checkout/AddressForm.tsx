import { Typography, Grid } from "@mui/material";
import { useFormContext } from "react-hook-form";
import AppCheckbox from "../../app/components/AppCheckbox";
import AppTextInput from "../../app/components/AppTextInput";

export default function AddressForm() {
  const { control, formState } = useFormContext();

  return (
    <>
      <Typography variant="h6" gutterBottom>
        Shipping Information
      </Typography>

      <Grid container spacing={3}>
        <Grid item xs={12} sm={12}>
          <AppTextInput control={control} name="fullName" label="Full Name" />
        </Grid>
        <Grid item xs={12}>
          <AppTextInput control={control} name="address" label="Shipping Address" />
        </Grid>
        <Grid item xs={12} sm={6}>
          <AppTextInput control={control} name="phoneNumber" label="Phone Number" />
        </Grid>
      </Grid>

      <Grid item xs={12}>
        <AppCheckbox
          disabled={!formState.isDirty}
          name="saveAddress"
          label="Save this as the default address"
          control={control}
        />
      </Grid>
    </>
  );
}
