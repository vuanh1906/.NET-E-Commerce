import { Box, Typography, Pagination } from "@mui/material";
import { MetaData } from "../models/pagination";

interface Props {
    metaData: MetaData;
    onPageChange: (page: number) => void;
}

export default function AppPagination({metaData, onPageChange}: Props) {
    const {totalPage, count, pageIndex, pageSize} = metaData;
    return (
        <Box display='flex' justifyContent='space-between' alignItems='center'>
        <Typography>Displaying 
            {(pageIndex - 1)*pageSize+1}-
            {pageIndex*pageSize > count ? count : pageIndex*pageSize} of {count} items
        </Typography>
        <Pagination
            color='secondary'
            size='large'
            count={totalPage}
            page={pageIndex}
            onChange={(e, page) => onPageChange(page)}
        />
    </Box>
    )
}