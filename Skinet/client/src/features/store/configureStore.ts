import { configureStore } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { accountSlice } from "../account/accountSlice";
import { basketSlice } from "../basket/BasketSlice";
import { catalogSlice } from "../catalog/catalogSlice";
import { counterSlice } from "../contact/counterSlice";

export const store = configureStore({
    reducer: {
        counter: counterSlice.reducer,
        basket: basketSlice.reducer,
        catalog : catalogSlice.reducer,
        account: accountSlice.reducer
    }
})
export type RootState = ReturnType<typeof store.getState>;
export type AppDisPatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDisPatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;