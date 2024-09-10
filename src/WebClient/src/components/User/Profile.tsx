import React, { useState } from "react";
import {
  Alert,
  Avatar,
  Box,
  Button,
  Container,
  Grid,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";

import useCustomSelector from "../../hooks/useCustomSelector";
import useAppDispatch from "../../hooks/useAppDispatch";
import { deleteUser, logout, updateUser } from "../../redux/reducers/usersReducer";
import { Link, useNavigate } from "react-router-dom";


const Profile = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const currentUser = useCustomSelector((state) => state.users.currentUser);

  const [editMode, setEditMode] = useState(false);
  const [editedValues, setEditedValues] = useState({
    userName: currentUser?.userName || ""
  });
  const [showSuccessAlert, setShowSuccessAlert] = useState(false);

  const handleEditedChange = (field: string, value: string) => {
    setEditedValues((prev) => ({ ...prev, [field]: value }));
  };

  const handleDelete = async () => {
    if (currentUser) {
      const payload = {
        id: currentUser.id,
        token: currentUser.token,
      };
      const action = await dispatch(deleteUser(payload));
      if (deleteUser.fulfilled.match(action)) {
        dispatch(logout());
        navigate("/");
      }
    }
  };

  if (!currentUser) {
    return (
      <>
        <Typography variant="body1" align="center">
          Please login to view your profile.
        </Typography>
        <Box style={{ textAlign: 'center', marginTop: '16px' }}>
          <Link to="/signin" style={{ textDecoration: 'none', color: 'blue' }}>
            Login
          </Link>
        </Box>
      </>
    );
  }

  const handleSubmit = async () => {
    if (currentUser) {
      const payload = {
        id: currentUser.id,
        userName: editedValues.userName,
        token: currentUser.token,
      };
      const action = await dispatch(updateUser(payload));
      if (updateUser.fulfilled.match(action)) {
        setShowSuccessAlert(true);
        setEditMode(false);
        setTimeout(() => {
          setShowSuccessAlert(false);
        }, 3000);
      }
    }
  };

  return (
    <Container component="main" maxWidth="xs">
      <Paper elevation={3} sx={{ padding: 3, marginTop: 4 }}>
        <Avatar sx={{ bgcolor: "secondary.main", width: 60, height: 60 }}>
          {currentUser?.userName[0].toUpperCase()}
        </Avatar>
        <Typography variant="h5" sx={{ marginBottom: 2 }}>
          Profile
        </Typography>
        <Typography variant="h4" component="h4" textAlign="center" marginBottom={2}>
          Hi {currentUser.userName} &#128075;
        </Typography>
        <Grid container spacing={2}>
          <Grid item xs={12} sm={6}>
            <TextField
              fullWidth
              label="First Name"
              value={editMode ? editedValues.userName : currentUser?.userName}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("userName", e.target.value)}
            />
          </Grid>
          <Grid item xs={12} sm={6}>
            <TextField
              fullWidth
              label="User Name"
              value={editMode ? editedValues.userName : currentUser?.userName}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("userName", e.target.value)}
            />
          </Grid>
        </Grid>
        <Grid>
          <Typography>{currentUser?.email}</Typography>
        </Grid>
        <Box sx={{ marginTop: 2 }}>
          {editMode ? (
            <Button variant="contained" color="primary" onClick={handleSubmit}>
              Save
            </Button>
          ) : (
            <>
              <Button
                variant="outlined"
                color="primary"
                startIcon={<Edit />}
                onClick={() => setEditMode(true)}
              >
                Edit
              </Button>
              {showSuccessAlert && (
                <Alert severity="success">Profile updated successfully!</Alert>
              )}
            </>
          )}
        </Box>
        <Button
          variant="contained"
          color="error"
          fullWidth={true}
          startIcon={<Delete />}
          sx={{ marginTop: 2, padding: "8px 16px" }}
          onClick={handleDelete}
        >
          Delete Profile
        </Button>
      </Paper>
    </Container>
  );
};

export default Profile;
