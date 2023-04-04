import { debounce, TextField } from "@mui/material";
import { useState } from "react";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import { setProductParams } from "./catalogSlice";

export default function ProductSearch() {
    const {productParams} = useAppSelector(state => state.catalog);
    const [search, setSearch] = useState(productParams.search);
    const dispatch = useAppDispatch();

    const debouncedSearch = debounce((event: any) => {
        dispatch(setProductParams({search: event.target.value}))
    }, 2000)

    return (
        <TextField
            label='Search products'
            variant='outlined'
            fullWidth
            value={search || ''}
            onChange={(event: any) => {
                setSearch(event.target.value);
                debouncedSearch(event);
            }}
        />
    )
}