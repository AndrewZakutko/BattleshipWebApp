import React from 'react'

interface Props {
    id: string | null;
    x: number;
    y: number;
    status: string;
}

export default function Cell(props: Props) {
  return (
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
        <th style={{background: "green"}} key={props.id}></th>
      )
      :
      (
        null
      )}
      {props.status == "Forbidden" ? 
      (
        <th style={{background: "gray"}} key={props.id}></th>
      )
      :
      (
        null
      )}
      {props.status == "ShootWithoutHit" ? 
      (
        <th key={props.id}></th>
      )
      :
      (
        null
      )}
      {props.status == "Destroyed" ? 
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
