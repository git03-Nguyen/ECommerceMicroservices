import { Edit, Delete } from "@mui/icons-material";
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
} from "@mui/material";
import { useState } from "react";
import agent from "../../../app/api/agent";
import { useAppDispatch } from "../../../app/store/configureStore";
import { removeCategory } from "../../catalog/catalogSlice";
import CategoryForm from "./CategoryForm";
import useCategories from "../../../app/hooks/useCategories";
import { Category } from "../../../app/models/categories"; // Assuming you have a Category model

export default function CategoryInventory() {
  const { categories } = useCategories();
  const dispatch = useAppDispatch();
  const [editMode, setEditMode] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState<Category | undefined>(
    undefined
  );
  const [loading, setLoading] = useState(false);
  const [target, setTarget] = useState<number | null>(null);

  function handleSelectCategory(category: Category) {
    setSelectedCategory(category);
    setEditMode(true);
  }

  function handleDeleteCategory(id: number) {
    setLoading(true);
    setTarget(id);
    agent.Admin.deleteCategory(id) // Assuming this is the API endpoint for deleting a category
      .then(() => dispatch(removeCategory(id)))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
  }

  function cancelEdit() {
    setSelectedCategory(undefined);
    setEditMode(false);
  }

  if (editMode)
    return <CategoryForm category={selectedCategory} cancelEdit={cancelEdit} />;

  return (
    <>
      <Box display="flex" justifyContent="space-between">
        <Typography sx={{ p: 2 }} variant="h4">
          Category Inventory
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
              <TableCell align="left">Category Name</TableCell>
              <TableCell align="center">Description</TableCell>
              <TableCell align="right"></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {categories.map((category: Category) => (
              <TableRow
                key={category.categoryId}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {category.categoryId}
                </TableCell>
                <TableCell align="left">{category.name}</TableCell>
                <TableCell align="center">{category.description}</TableCell>
                <TableCell align="right">
                  <Button
                    onClick={() => handleSelectCategory(category)}
                    startIcon={<Edit />}
                  />
                  <LoadingButton
                    loading={loading && target === category.categoryId}
                    onClick={() => handleDeleteCategory(category.categoryId)}
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