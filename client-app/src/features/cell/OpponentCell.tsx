import { observer } from "mobx-react-lite";
import React from "react";

interface Props {
    id: string | null;
    x: number;
    y: number;
    status: string;
}

export default observer(function OpponentCell(props: Props){
    return(
        <>
            {props.status == "None" ? 
            (
                <th key={props.id}></th>
            )
            :
            (
                null
            )}
            {props.status == "Busy" ? 
            (
                <th key={props.id}></th>
            )
            :
            (
                null
            )}
            {props.status == "Forbidden" ? 
            (
                <th key={props.id}></th>
            )
            :
            (
                null
            )}
            {props.status == "ShootWithoutHit" ? 
            (
                <th style={{background: "red"}} key={props.id}></th>
            )
            :
            (
                null
            )}
            {props.status == "Destroyed" ? 
            (
                <th style={{background: "blue"}} key={props.id}></th>
            )
            :
            (
                null
            )}
        </>
    )
})