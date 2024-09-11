import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { Link, useNavigate } from 'react-router-dom';
import useAppDispatch from '../../hooks/useAppDispatch';
import { useState } from 'react';
import { registerUser } from '../../redux/reducers/usersReducer';
import { Alert, MenuItem } from '@mui/material';

import Copyright from '../Shared/Copyright';

// TODO remove, this demo shouldn't need to reset the theme.
const defaultTheme = createTheme();

export default function SignUp() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [userName, setUserName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [fullName, setFullName] = useState("");
  const [address, setAddress] = useState("");
  const [phoneNumber, setPhoneNumber] = useState("");
  const [role, setRole] = useState("");
  const [showAlert, setShowAlert] = useState(false);
  const [alertMessage, setAlertMessage] = useState("");

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    try {
      const action = await dispatch(registerUser({ userName, email, password, fullName, address, phoneNumber, role: 'Customer' }));
      if (registerUser.fulfilled.match(action)) {
        if (typeof action.payload === "string") {
          setShowAlert(true);
          console.log(action.payload);
          setAlertMessage(action.payload);
        } else {
          navigate("/signin");
        }
      } else {
        setShowAlert(true);
        setAlertMessage("Invalid data passed.");
      }
    }
    catch (error) {
      setShowAlert(true);
      setAlertMessage("Error occured while creating user.");
    }
  };

  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign up
          </Typography>
          {showAlert && (
            <Alert variant="outlined" severity="error">
              {alertMessage}
            </Alert>
          )}
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="userName"
                  label="User Name"
                  name="userName"
                  autoComplete="username"
                  value={userName}
                  onChange={(l) => setUserName(l.target.value)}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="email"
                  label="Email Address"
                  name="email"
                  autoComplete="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  autoComplete="new-password"
                  value={password}
                  onChange={(p) => setPassword(p.target.value)}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="fullName"
                  label="Full Name"
                  id="fullName"
                  value={fullName}
                  onChange={(f) => setFullName(f.target.value)}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="address"
                  label="Address"
                  id="address"
                  value={address}
                  onChange={(a) => setAddress(a.target.value)}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="phoneNumber"
                  label="Phone Number"
                  id="phoneNumber"
                  value={phoneNumber}
                  onChange={(p) => setPhoneNumber(p.target.value)}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  select
                  name="role"
                  label="Role"
                  id="role"
                  value={role}
                  onChange={(r) => setRole(r.target.value)}
                >
                  <MenuItem value="Customer">Customer</MenuItem>
                  <MenuItem value="Seller">Seller</MenuItem>
                </TextField>
              </Grid>
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Sign Up
            </Button>
            <Grid container justifyContent="flex-end">
              <Grid item>
                <Link to={"/signin"}>
                  Already have an account? Sign in
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
        <Copyright sx={{ mt: 5 }} />
      </Container>
    </ThemeProvider>
  );
}