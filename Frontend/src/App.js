import React, { useState, useEffect } from "react";
import Container from "@mui/material/Container";
import { Button, Typography } from "@mui/material";

export const App = () => {
    const [price, setPrice] = useState(null);
    const [error, setError] = useState(null);
    const updatePrice = async () => {
        let priceResponse = await fetch("/api/Development");

        if(priceResponse.ok) {
            setError(null);
            setPrice(await priceResponse.json());
        } else {
            setError("Unable to fetch price from API.");
        }
    };
    useEffect(() => updatePrice(), []);

    return (
        <Container>
            {
                price &&
                <>
                    <Typography>Symbol {price.symbol}</Typography>
                    <Typography>Price {price.price}</Typography>
                </>
            }
            {
                error &&
                <Typography>Error: {error}</Typography>
            }
            <Button onClick={updatePrice}>
                Update price
            </Button>
        </Container>
    );
}