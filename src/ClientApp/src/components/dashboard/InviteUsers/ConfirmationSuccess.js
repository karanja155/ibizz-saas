import React from 'react'
import { FcCheckmark } from 'react-icons/fc'

export const ConfirmationSuccess = ({handleModalReset, handleDashboard}) => {
  return (
    <>
        <div className='flex justify-center'><FcCheckmark fontSize={50} /></div>
        <p className='flex justify-center font-semibold text-lg'>Invite sent successfully</p>
        <p className='flex justify-center text-sm'>You will receive a notification once the user confirms the invitation.</p>
        <div className='flex gap-4 justify-center'>
            <button className='p-2 rounded box-border text-xs bg-bg text-white hover:bg-blueLight' onClick={() => {handleDashboard()}}>Dashboard</button>
            <button className='p-2 rounded box-border text-xs bg-bg text-white hover:bg-blueLight' onClick={() => {handleModalReset()}}>Invite another user</button>
        </div>
    </>
  )
}
