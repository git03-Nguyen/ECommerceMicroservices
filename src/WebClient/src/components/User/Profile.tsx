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

import useCustomSelector from "../../hooks/useCustomSelector";
import useAppDispatch from "../../hooks/useAppDispatch";
import { deleteUser, logout, updateUser } from "../../redux/reducers/usersReducer";
import { Link, useNavigate } from "react-router-dom";
import { getFirstCharOfName } from "../../helpers/stringHelpers";


const Profile = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const currentUser = useCustomSelector((state) => state.users.currentUser);

  const [editMode, setEditMode] = useState(false);
  const [editedValues, setEditedValues] = useState({
    userName: currentUser?.userName || "",
    email: currentUser?.email || "",
    fullName: currentUser?.fullName || "",
    phoneNumber: currentUser?.phoneNumber || "",
    address: currentUser?.address || "",
    paymentDetails: currentUser?.paymentDetails || "",
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
      const payload = { ...editedValues, id: currentUser.id };
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
        <Avatar sx={{ bgcolor: "secondary.main", width: 60, height: 60, margin: "0 auto", marginBottom: 2 }} >
          {getFirstCharOfName(currentUser?.fullName)}
        </Avatar>
        <Typography variant="h4" component="h4" textAlign="center" marginBottom={0.5}>
          Hi {currentUser.fullName} &#128075;
        </Typography>
        <Typography variant="body2" component="p" textAlign="center" marginBottom={2}>
          <i>({currentUser.role})</i>
        </Typography>
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="User Name"
              value={editMode ? editedValues.userName : currentUser?.userName}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("userName", e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="Email"
              value={editMode ? editedValues.email : currentUser?.email}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("userName", e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="Full Name"
              value={editMode ? editedValues.fullName : currentUser?.fullName}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("fullName", e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="Phone Number"
              value={editMode ? editedValues.phoneNumber : currentUser?.phoneNumber}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("phoneNumber", e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              required
              fullWidth
              label="Address"
              value={editMode ? editedValues.address : currentUser?.address}
              disabled={!editMode}
              onChange={(e) => handleEditedChange("address", e.target.value)}
            />
          </Grid>
          <Grid item xs={12}>
            <TextField
              fullWidth
              select
              label="Payment Details"
              value={editMode ? editedValues.paymentDetails : currentUser?.paymentDetails}
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
        <Box sx={{ marginTop: 2 }}>
          {editMode ?
            (
              <Button fullWidth variant="contained" color="primary" onClick={handleSubmit}>
                Save
              </Button>
            ) : (
              <>
                <Button
                  fullWidth
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
