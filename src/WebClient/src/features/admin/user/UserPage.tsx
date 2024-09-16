import { Edit, Delete, LockOutlined } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import {
  Box,
  Typography,
  Button,
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Avatar,
} from "@mui/material";
import { useEffect, useState } from "react";
import agent from "../../../app/api/agent";
import { useAppDispatch, useAppSelector } from "../../../app/store/configureStore";
import UserForm from "./UserForm";
import { User } from "../../../app/models/user";
import { toast } from "react-toastify";
import { signOut } from "../../account/accountSlice";

const catalogUrl = process.env.REACT_APP_CATALOG_URL!;

export default function UserPage() {
  const dispatch = useAppDispatch();
  const currentUser = useAppSelector((state) => state.account.user);
  console.log("currentUser", currentUser);
  const [editMode, setEditMode] = useState(false);
  const [selectedUser, setselectedUser] = useState<User | undefined>(
    undefined
  );
  const [loading, setLoading] = useState(false);
  const [target, setTarget] = useState<string | null>(null);
  const [usersList, setUsersList] = useState<User[]>([]);

  // const usersList = useAppSelector((state) => state.admin.users);

  // Fetch users from the API
  useEffect(() => {
    agent.Admin.listUsers()
      .then((response) => {
        setUsersList(response.payload);
      })
      .catch((error) => console.log(error));
  }, []);

  function handleSelectUser(user: User) {
    setselectedUser(user);
    setEditMode(true);
  }

  function handleDeleteUser(id: string) {
    setLoading(true);
    setTarget(id);
    if (currentUser?.userId == id) {
      toast.error("You can't delete yourself", {
        autoClose: 3000, // Set timeout in milliseconds
      });
      setLoading(false);
      setTarget(null);
      return;
    }
    agent.Admin.deleteUser(id) // Assuming this is the API endpoint for deleting a user
      .then(() => {
        toast.success("User deleted successfully");
        if (usersList) {
          setUsersList(usersList.filter((u) => u.userId !== id));
        }

      }
      )
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
  }

  function cancelEdit() {
    setselectedUser(undefined);
    setEditMode(false);
  }

  if (editMode)
    return <UserForm user={selectedUser} cancelEdit={cancelEdit} />;

  return (
    <>
      <Box display="flex" justifyContent="space-between">
        <Typography sx={{ p: 2 }} variant="h4">
          Users Management
        </Typography>
        <Button
          onClick={() => setEditMode(true)}
          sx={{ m: 2 }}
          variant="contained"
        >
          Create
        </Button>
      </Box>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Id</TableCell>
              <TableCell align="center">Full Name</TableCell>
              <TableCell align="left">UserName</TableCell>
              <TableCell align="center">Email</TableCell>
              <TableCell align="center">Role</TableCell>
              <TableCell align="center">Address</TableCell>
              <TableCell align="center">Phone</TableCell>
              <TableCell align="right"></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {usersList.map((u: User) => (
              <TableRow
                key={u.userId}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {u.userId}
                </TableCell>
                <TableCell align="left">
                  <Box display="flex" alignItems="center">
                    <Avatar
                      src={u.avatar?.startsWith("http") ? u.avatar : `${catalogUrl}${u.avatar}`}
                      alt={u.fullName}
                      sx={{
                        m: 1,
                        bgcolor: u.avatar ? "transparent" : "secondary.main", // Set background color only if image is not available
                        height: 40,
                        width: 40,
                        marginRight: 2,
                        objectFit: "contain",
                      }}
                    >
                      {!u.avatar && <LockOutlined />}
                    </Avatar>
                    <span style={{ width: "8rem" }}>
                      {u && u.fullName && u.fullName.length > 20
                        ? u.fullName?.slice(0, 20) + "..."
                        : u.fullName}
                    </span>
                  </Box>
                </TableCell>
                <TableCell align="left">{u.userName}</TableCell>
                <TableCell align="center">{u.email}</TableCell>
                <TableCell align="center">{u.role}</TableCell>
                <TableCell align="center">{u.address}</TableCell>
                <TableCell align="center">{u.phoneNumber}</TableCell>
                <TableCell align="right">
                  <Button
                    onClick={() => handleSelectUser(u)}
                    startIcon={<Edit />}
                  />
                  <LoadingButton
                    loading={loading && target === u.userId}
                    onClick={() => handleDeleteUser(u.userId)}
                    startIcon={<Delete />}
                    color="error"
                  />
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}