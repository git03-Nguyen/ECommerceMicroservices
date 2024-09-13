import React, { useState } from "react";
import {
  Alert,
  Avatar,
  Box,
  Button,
  Container,
  Grid,
  MenuItem,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";

import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { fetchCurrentUser, signOut } from "../../features/account/accountSlice";
import { clearBasket } from "../../features/basket/basketSlice";
import { Link, useNavigate } from "react-router-dom";
import agent from "../../app/api/agent";
import { toast } from "react-toastify";


const Profile = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { user } = useAppSelector((state) => state.account);

  const [editMode, setEditMode] = useState(false);
  const [editedValues, setEditedValues] = useState({
    userName: user?.userName || "",
    email: user?.email || "",
    fullName: user?.fullName || "",
    phoneNumber: user?.phoneNumber || "",
    address: user?.address || "",
    paymentDetails: user?.paymentDetails || "",
  });
  const [showSuccessAlert, setShowSuccessAlert] = useState(false);

  const handleEditedChange = (field: string, value: string) => {
    setEditedValues((prev) => ({ ...prev, [field]: value }));
  };

  const handleDelete = async () => {
    if (user) {
      try {
        await agent.Admin.deleteUser(user.userId);
        dispatch(signOut());
        dispatch(clearBasket());
        toast.success("User deleted successfully");
        navigate("/");
      }
      catch (error) {
        toast.error(`Error: ${error}`);
        console.log(error);
      }
    }
  };

  const handleSubmit = async () => {
    if (user) {
      const payload = { ...editedValues, id: user.userId };
      try {
        await agent.Admin.updateUser(payload);
        setShowSuccessAlert(true);
        dispatch(fetchCurrentUser());
        setEditMode(false);
        setTimeout(() => {
          setShowSuccessAlert(false);
        }, 3000);
        // update current user in store
      }
      catch (error) {
        toast.error(`Error: ${error}`);
        console.log(error);
      }
    }
  };

  if (!user) {
    return (
      <>
        <Typography variant="body1" align="center">
          Please login to view your profile.
        </Typography>
        <Box style={{ textAlign: 'center', marginTop: '16px' }}>
          <Link to="/login" style={{ textDecoration: 'none', color: 'blue' }}>
            Login
          </Link>
        </Box>
      </>
    );
  }

  return (
    <Container component="main" maxWidth="sm">
      <Paper elevation={3} sx={{ padding: 3, marginTop: 4 }}>
        <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginBottom: 2 }}>
          <Avatar sx={{ bgcolor: "secondary.main", width: 60, height: 60 }}>
            {user && user.fullName ? user.fullName.charAt(0) : "U"}
          </Avatar>
          <Typography variant="h4" component="h4" textAlign="center" marginY={1}>
            Hi {user.fullName} &#128075;
          </Typography>
          <Typography variant="body2" component="p" textAlign="center" color="textSecondary">
            <i>({user.role})</i>
          </Typography>
        </Box>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="User Name"
              value={editMode ? editedValues.userName : user?.userName}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("userName", e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="Email"
              value={editMode ? editedValues.email : user?.email}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("email", e.target.value)} // Fixed typo here
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="Full Name"
              value={editMode ? editedValues.fullName : user?.fullName}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("fullName", e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="Phone Number"
              value={editMode ? editedValues.phoneNumber : user?.phoneNumber}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("phoneNumber", e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="Address"
              value={editMode ? editedValues.address : user?.address}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("address", e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              fullWidth
              select
              label="Payment Details"
              value={editMode ? editedValues.paymentDetails : user?.paymentDetails}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("paymentDetails", e.target.value)}
            >
              <MenuItem value="">None</MenuItem>
              <MenuItem value="COD">Debit Card</MenuItem>
              <MenuItem disabled value="Credit Card">Credit Card</MenuItem>
              <MenuItem disabled value="Paypal">Paypal</MenuItem>
            </TextField>
          </Grid>
        </Grid>
        <Grid container spacing={2} sx={{ marginTop: 2 }}>
          <Grid item xs={6}>
            {editMode ? (
              <Button
                fullWidth
                variant="contained"
                color="primary"
                onClick={handleSubmit}
              >
                Save
              </Button>
            ) : (
              <Button
                fullWidth
                variant="outlined"
                color="primary"
                startIcon={<Edit />}
                onClick={() => setEditMode(true)}
              >
                Edit
              </Button>
            )}
          </Grid>

          <Grid item xs={6}>
            <Button
              fullWidth
              variant="contained"
              color="error"
              startIcon={<Delete />}
              onClick={handleDelete}
            >
              Delete Profile
            </Button>
          </Grid>

          {showSuccessAlert && !editMode && (
            <Grid item xs={12}>
              <Alert severity="success" sx={{ width: '100%', marginTop: 2 }}>
                Profile updated successfully!
              </Alert>
            </Grid>
          )}
        </Grid>


      </Paper>
    </Container>
  );
};

export default Profile;
