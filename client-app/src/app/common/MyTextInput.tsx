import React from "react";

interface Props{
    placeholder: string;
    name: string;
    type?: string;
    label?: string;
}

export default function MyTextInput(props: Props) {
    return(
        <div>
            <label className='text-field__label'>
                {props.label}
                <input
                    placeholder={props.placeholder}
                    className='text-field__input'
                    name={props.name}
                    type={props.type}
                />
            </label>
        </div>
    )
}