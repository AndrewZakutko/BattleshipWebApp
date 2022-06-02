import React from "react";
import { useStore } from "../../app/stores/store";
import * as Yup from 'yup';
import { Formik } from "formik";
import { observer } from "mobx-react-lite";
import AddShip from "../../app/models/addShip";

export default observer(function AddShipForm() {
    const { userStore, gameStore, cellStore } = useStore();

    const validate = Yup.object({
        startPositionX: Yup.number().max(9, "Max X is 9!").min(0, "Min Y is 0!").required("X must be required!"),
        startPositionY: Yup.number().max(9, "Max X is 9!").min(0, "Min Y is 0!").required("Y must be required!"),
        direction: Yup.string().required("Direction must be required!"),
        rank: Yup.string().required("Rank must be required!")
    })

    return(
        <>
            <Formik
                initialValues={{
                    fieldId: userStore.fieldId,
                    startPositionX: 0,
                    startPositionY: 0,
                    direction: '',
                    rank: ''
                }}
                onSubmit={(values, {setSubmitting}) => {
                    gameStore.addShip(values as AddShip);
                    setSubmitting(false);
                }}
                validationSchema = {validate}
            >
                {({values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting}) => (
                    <form onSubmit={handleSubmit}>
                        <label className='text-field__label'>Start position:</label>
                        <input
                            placeholder="X"
                            className='text-field__input'
                            name="startPositionX"
                            type="number"
                            value={values.startPositionX}
                            onBlur={handleBlur}
                            onChange={handleChange}
                        />
                        <div>
                            {errors.startPositionX && touched.startPositionX}
                        </div>
                        <input
                            placeholder="Y"
                            className='text-field__input'
                            name="startPositionY"
                            type="number"
                            value={values.startPositionY}
                            onBlur={handleBlur}
                            onChange={handleChange}
                        />
                        <div>
                            {errors.startPositionY && touched.startPositionY}
                        </div>
                        <label className='text-field__label'>Direction:</label>
                        <select onChange={handleChange} value={values.direction} className="select-css" name="direction">
                            <option>Choose direction</option>
                            <option value='Horizontal'>Horizontal</option>
                            <option value='Vertical'>Vertical</option>
                        </select>
                        <div>
                            {errors.direction && touched.direction}
                        </div>
                        <label className='text-field__label'>Rank:</label>
                        <select onChange={handleChange} value={values.rank} className="select-css" name="rank">
                            <option>Choose rank</option>
                            <option value='One'>One</option>
                            <option value="Two">Two</option>
                            <option value="Three">Three</option>
                            <option value="Four">Four</option>
                        </select>
                        <div>
                            {errors.rank && touched.rank}
                        </div>
                        <button className="btn" disabled={isSubmitting} style={{marginTop: '10px'}} type='submit'>Add ship</button>
                    </form>
                )}
            </Formik>
        </>
    )
})