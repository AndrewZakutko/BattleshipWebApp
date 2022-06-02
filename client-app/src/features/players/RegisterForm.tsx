import { Formik, useFormik } from 'formik';
import React from 'react'
import { Button, Checkbox, Form } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';
import * as Yup from 'yup';

export default function RegisterForm() {
    const { userStore } = useStore();

    const validate = Yup.object({
        name: Yup.string().required('Required'),
        username: Yup.string().required('Required'),
        email: Yup.string().email('Email is invalid').required('Required'),
        password: Yup.string().required('Required').min(6, "Min length of pass 6 words")
    })

    return(
        <Formik
            initialValues={{
                email: '', 
                password: '',
                name: '',
                username: ''
            }}
            onSubmit={(values, {setSubmitting}) => {
                userStore.register(values);
                setSubmitting(false);
            }}
            validationSchema={validate}
        >
            {({values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting}) => (
                <form onSubmit={handleSubmit}>
                    <label className='text-field__label'>Name:</label>
                    <input
                        placeholder="Name"
                        className='text-field__input'
                        name="name"
                        type="text"
                        value={values.name}
                        onBlur={handleBlur}
                        onChange={handleChange}
                    />
                    <div>
                        {errors.name && touched.name && errors.name}
                    </div>
                    <label className='text-field__label'>Username:</label>
                    <input
                        placeholder="Username"
                        className='text-field__input'
                        name="username"
                        type="text"
                        value={values.username}
                        onBlur={handleBlur}
                        onChange={handleChange}
                    />
                    <div>
                        {errors.username && touched.username && errors.username}
                    </div>
                    <label className='text-field__label'>Email:</label>
                    <input
                        placeholder="Email"
                        className='text-field__input'
                        name="email"
                        type="text"
                        value={values.email}
                        onBlur={handleBlur}
                        onChange={handleChange}
                    />
                    <div>
                        {errors.email && touched.email && errors.email}
                    </div>
                    <label className='text-field__label'>Password:</label>
                    <input
                        placeholder="Password"
                        className='text-field__input'
                        name="password"
                        type="password"
                        onBlur={handleBlur}
                        value={values.password}
                        onChange={handleChange}
                    />
                    <div>
                        {errors.password && touched.password && errors.password}
                    </div>
                    <Button positive disabled={isSubmitting} style={{marginTop: '10px'}} type='submit'>Register</Button>
                </form>
            )}
        </Formik>
    )
}
