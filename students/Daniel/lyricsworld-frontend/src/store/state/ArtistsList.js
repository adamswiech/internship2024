import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";

export const getArtists = createAsyncThunk('artist/fetch', async () => {
    const Iresponse = await axios.get("http://localhost:5159/api/Artists/GetArtist/")
    return Iresponse.data
})

export const ArtistsList = createSlice({
    name: "artist",
    initialState:
    {
        data: [],
        error: ''
    },
    extraReducers: (builder) => {
        builder
        .addCase(getArtists.pending, () => {console.log("fetching data")})
        .addCase(getArtists.fulfilled, (state, action) => {state.data.push(action.payload)})
    }
})

export default ArtistsList.reducer