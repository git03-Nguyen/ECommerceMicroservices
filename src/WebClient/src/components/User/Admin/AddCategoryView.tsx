import React, { useState } from 'react'
import { Alert, AlertColor, Box, Button, Container, CssBaseline, Grid, TextField, ThemeProvider, Typography } from '@mui/material';

import useAppDispatch from '../../../hooks/useAppDispatch';
import { createCategory } from '../../../redux/reducers/categoriesReducer';
import { defaultTheme } from '../../../styles/Component/Shared';

const AddCategoryView = () => {
  const dispatch = useAppDispatch();
  const [name, setName] = useState("");
  const [imageUrl, setImageUrl] = useState("");
  const [description, setDescription] = useState("");
  const [showAlert, setShowAlert] = useState(false);
  const [alertSeverity, setAlertSeverity] = useState<AlertColor>("info");
  const [alertMessage, setAlertMessage] = useState("");

  const handleSuccess = () => {
    setName("");
    setImageUrl("");
    setDescription("");

    setShowAlert(true);
    setAlertSeverity("success");
    setAlertMessage("Category created successfully!");
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      const action = await dispatch(
        createCategory({ name, description, imageUrl })
      );
      if (createCategory.fulfilled.match(action)) {
        if (typeof action.payload === "string") {
          setShowAlert(true);
          setAlertSeverity("error");
          setAlertMessage(action.payload);
        } else {
          handleSuccess();
        }
      } else {
        setShowAlert(true);
        setAlertSeverity("error");
        setAlertMessage("Invalid data passed.");
      }
    } catch (error) {
      setShowAlert(true);
      setAlertSeverity("error");
      setAlertMessage("Error occured while creating category.");
    }
  };

  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component={"main"} maxWidth="xs">
        <CssBaseline />
        <Typography variant="h4" gutterBottom>Add New Category</Typography>
        {showAlert && <Alert severity={alertSeverity}>{alertMessage}</Alert>}
        <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
          <Grid container spacing={2} marginTop={1}>
            <Grid item xs={12}>
              <TextField
                name="Name"
                required
                fullWidth
                id="name"
                label="Category Name"
                value={name}
                onChange={(n) => setName(n.target.value)}
                autoFocus
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                required
                fullWidth
                id="description"
                label="Category Description"
                name="description"
                value={description}
                onChange={(desc) => setDescription(desc.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                required
                fullWidth
                id="imageUrl"
                label="Category Image URL"
                name="imageUrl"
                value={imageUrl}
                onChange={(img) => setImageUrl(img.target.value)}
              />
            </Grid>
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Create Category
          </Button>
        </Box>
      </Container>
    </ThemeProvider>
  )
}

export default AddCategoryView