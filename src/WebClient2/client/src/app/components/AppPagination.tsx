import { Box, Typography, Pagination } from "@mui/material";
import { useState } from "react";
import { MetaData } from "../models/pagination";

interface Props {
  metaData: MetaData;
  onPageChange: (page: number) => void;
}

export default function AppPagination({ metaData, onPageChange }: Props) {
  const { pageSize, pageNumber, totalPage, totalCount } = metaData;
  const [currentPage, setCurrentPage] = useState(pageNumber);

  function handlePageChange(page: number) {
    setCurrentPage(page);
    onPageChange(page);
  }

  console.log(metaData);

  return (
    totalCount == 0 ? (
      <Typography variant="body1" sx={{ fontStyle: 'italic', textAlign: 'center' }}>
        No results
      </Typography>
    ) : (
      <Box
        display="flex"
        justifyContent="space-between"
        alignItems="center"
        sx={{ marginBottom: 3 }}
      >
        <Typography variant="body1">
          Displaying {(currentPage - 1) * pageSize + 1}-
          {currentPage * pageSize > totalCount!
            ? totalCount
            : currentPage * pageSize}{" "}
          of {totalCount} results
        </Typography>
        <Pagination
          color="secondary"
          size="large"
          count={totalPage}
          page={currentPage}
          onChange={(e, page) => handlePageChange(page)}
        />
      </Box>
    )
  );
}
