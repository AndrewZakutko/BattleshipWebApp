import React from 'react'

interface Props {
    id: string | null;
    x: number;
    y: number;
    cellStatus: string;
}

export default function Cell(props: Props) {
  return (
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
        <th style={{background: "green"}} key={props.id}></th>
      )
      :
      (
        null
      )}
      {props.cellStatus == "Forbidden" ? 
      (
        <th style={{background: "gray"}} key={props.id}></th>
      )
      :
      (
        null
      )}
      {props.cellStatus == "ShootWithoutHit" ? 
      (
        <th key={props.id}></th>
      )
      :
      (
        null
      )}
      {props.cellStatus == "Destroyed" ? 
      (
        <th key={props.id}></th>
      )
      :
      (
        null
      )}
    </>
  )
}
