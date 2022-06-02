import React from 'react';
import * as Yup from 'yup';
import { Formik } from "formik";
import { observer } from "mobx-react-lite";
import { useStore } from '../../app/stores/store';
import { Shoot } from '../../app/models/shoot';

export default observer(function ShootForm() {
    const { userStore, cellStore } = useStore();

    var fieldId: string | undefined = undefined;
    var firstPlayerName: string | undefined = undefined
    var secondPlayerName: string | undefined = undefined

    if(userStore.user!.name == userStore.game!.firstPlayerName)
    {
        fieldId = userStore.game!.secondPlayerFieldId;
        firstPlayerName = userStore.game!.firstPlayerName;
        secondPlayerName = userStore.game!.secondPlayerName;
    }
    if(userStore.user!.name == userStore.game!.secondPlayerName)
    {
        fieldId = userStore.game!.firstPlayerFieldId;
        firstPlayerName = userStore.game!.secondPlayerName;
        secondPlayerName = userStore.game!.firstPlayerName;
    }

    const validate = Yup.object({
        x: Yup.number().max(9, "Max X is 9!").min(0, "Min Y is 0!").required("X must be required!"),
        y: Yup.number().max(9, "Max X is 9!").min(0, "Min Y is 0!").required("Y must be required!"),
    })

    return(
        <>
            <Formik
                initialValues={{
                    fieldId: fieldId,
                    firstPlayerName: firstPlayerName,
                    secondPlayerName: secondPlayerName,
                    x: 0,
                    y: 0,
                }}
                onSubmit={(values, {setSubmitting}) => {
                    userStore.shoot(values as Shoot);
                    setSubmitting(false);
                }}
                validationSchema = {validate}
            >
                {({values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting}) => (
                    <form onSubmit={handleSubmit}>
                        <label className='text-field__label'>Position:</label>
                        <input
                            placeholder="X"
                            className='text-field__input'
                            name="x"
                            type="number"
                            value={values.x}
                            onBlur={handleBlur}
                            onChange={handleChange}
                        />
                        <div>
                            {errors.x && touched.x}
                        </div>
                        <input
                            placeholder="Y"
                            className='text-field__input'
                            name="y"
                            type="number"
                            value={values.y}
                            onBlur={handleBlur}
                            onChange={handleChange}
                        />
                        <div>
                            {errors.y && touched.y}
                        </div>
                        <button className="btn" disabled={isSubmitting} style={{marginTop: '10px'}} type='submit'>Shoot</button>
                    </form>
                )}
            </Formik>
        </>
    )
})