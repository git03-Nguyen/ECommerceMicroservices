import { Button, Menu, Fade, MenuItem, ListItemIcon, ListItemText } from "@mui/material";
import { useState } from "react";
import { Link } from "react-router-dom";
import { signOut } from "../../features/account/accountSlice";
import { clearBasket } from "../../features/basket/basketSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import { AccountCircle, Logout, ReceiptLong } from "@mui/icons-material";

export default function SignedInMenu() {
  const dispatch = useAppDispatch();
  const { user } = useAppSelector((state) => state.account);
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: any) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <>
      <Button color="inherit" onClick={handleClick} sx={{ typography: "h6" }}>
        {user?.userName}
      </Button>
      <Menu
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        TransitionComponent={Fade}
      >
        <MenuItem component={Link} to="/profile" onClick={handleClose}>
          <ListItemIcon>
            <AccountCircle fontSize="small" />
          </ListItemIcon>
          <ListItemText primary="Profile" />
        </MenuItem>
        {user && user.role == "Customer" && (
          <MenuItem component={Link} to="/orders" onClick={handleClose}>
            <ListItemIcon>
              <ReceiptLong fontSize="small" />
            </ListItemIcon>
            <ListItemText primary="My Orders" />
          </MenuItem>
        )}
        <MenuItem
          onClick={() => {
            dispatch(signOut());
            dispatch(clearBasket());
          }}
        >
          <ListItemIcon>
            <Logout fontSize="small" />
          </ListItemIcon>
          <ListItemText primary="Logout" />
        </MenuItem>
      </Menu>
    </>
  );
}
