import { observer } from "mobx-react-lite";
import React from "react";

interface Props {
    id: string | null;
    x: number;
    y: number;
    cellStatus: string;
}

export default observer(function PlayerCell(props: Props){
    return(
        <>
            {props.cellStatus == "None" ? 
            (
                <th key={props.id}></th>
            )
            :
            (
                null
            )}
            {props.cellStatus == "Busy" ? 
            (
                <th style={{background: "brown", opacity: "0.5"}} key={props.id}></th>
            )
            :
            (
                null
            )}
            {props.cellStatus == "Forbidden" ? 
            (
                <th key={props.id}></th>
            )
            :
            (
                null
            )}
            {props.cellStatus == "ShootWithoutHit" ? 
            (
                <th style={{background: "red"}} key={props.id}></th>
            )
            :
            (
                null
            )}
            {props.cellStatus == "Destroyed" ? 
            (
                <th style={{background: "brown"}} key={props.id}></th>
            )
            :
            (
                null
            )}
        </>
    )
})