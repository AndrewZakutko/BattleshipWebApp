import { Formik, useFormik } from 'formik';
import { observer } from 'mobx-react-lite';
import React from 'react';
import { Button } from 'semantic-ui-react';
import { useStore } from '../../app/stores/store';
import * as Yup from 'yup'

export default observer(function LoginForm() {
    const { userStore } = useStore();

    const validate = Yup.object({
        email: Yup.string().email('Email is invalid').required('Required'),
        password: Yup.string().required('Required')
    })
    return( 
        <div className='text-field'>
            <h1>Login</h1>
            <Formik
                initialValues={{email: '', password: ''}}
                onSubmit={(values, {setSubmitting}) => {
                    userStore.login(values);
                    setSubmitting(false);
                }}
                validationSchema={validate}
            >
                {({values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting}) => (
                    <form onSubmit={handleSubmit}>
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
                        <Button positive disabled={isSubmitting} style={{marginTop: '10px'}} type='submit'>Login</Button>
                    </form>
                )}
            </Formik>
        </div>
    )
})