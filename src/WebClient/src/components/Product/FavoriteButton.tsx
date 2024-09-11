import React from 'react'
import useAppDispatch from '../../hooks/useAppDispatch'
import { FavoriteButtonProps } from '../../type/Product'
import useCustomSelector from '../../hooks/useCustomSelector'
import { addToFavorites, removeFromFavorites } from '../../redux/reducers/favoriteReducer'
import { IconButton } from '@mui/material'
import { Favorite, FavoriteBorder } from '@mui/icons-material'

const FavoriteButton = ({ favProduct }: FavoriteButtonProps) => {
    const dispatch = useAppDispatch()
    const isFavorite = useCustomSelector((state) =>
        state.favorites.some((product: { productId: number }) => product.productId === favProduct.productId));

    const toggleFavorite = () => {
        if (isFavorite) {
            dispatch(removeFromFavorites({ productId: favProduct.productId }))
        } else {
            dispatch(addToFavorites(favProduct))
        }
    }
    return (
        <IconButton
            onClick={toggleFavorite}
            aria-label='toggle favorite'
        >
            {isFavorite ? <Favorite color='error' /> : <FavoriteBorder />}
        </IconButton>
    )
}

export default FavoriteButton