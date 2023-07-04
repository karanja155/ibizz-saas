import React, { useState } from 'react'
import { TextBox } from "devextreme-react/text-box";
import TextArea from "devextreme-react/text-area";
import { Button } from 'devextreme-react';
import { useNavigate } from 'react-router-dom';
import request from '../../../../helpers/tempRequest';
import { ConfirmationSuccess } from '../../../../components/dashboard/InviteUsers/ConfirmationSuccess';

const InviteUsers = () => {
  const [fullName, setfullName] = useState()
  const [email, setEmail] = useState()
  const [telephone, setTelephone] = useState()
  const [country, setCountry] = useState()
  const [extras, setExtras] = useState()
  const [modalView, setModalView] = useState(false)

  const navigate = useNavigate();

  const handleSubmit = async() => {
    const payload = {
      "FullName" : fullName,
      "EmailAddress" : email,
      "Telephone" : telephone,
      "Country" : country,
      "Extras" : extras
    }
    try {
      // const response = await request.post("http://localhost:5012/api/newInvite", payload)
      setModalView(true)
      console.log(modalView)

    } catch(e){
      console.log(e)

    }
  }

  const handleDashboard = () => {
    navigate('/dashboard');
  }

  const handleModalReset = () => {
    setCountry();
    setEmail();
    setExtras();
    setTelephone();
    setfullName();
    setModalView(!modalView);
  }

  return (
    <div className='flex justify-center relative'>
      <div className='w-2/3 p-6 flex flex-col gap-4'>
        <h2 className="text-3xl font-semibold text-headingBlue">Invite new user to your organization</h2>
        <p className='text-sm font-normal'>You can invite users to have various access rights to your organization here.</p>
        <TextBox
            type="text"
            id="fullName"
            label='Full names'
            // placeholder="User's full names"
            onValueChanged={(e) => setfullName(e.value)}
            value={fullName}
            height={40}
            style={{ fontSize: "12px" }}
            className=" border pl-1 w-full md:w-1/2 lg:w-[60%] xl:w-[80%] outline-none "
          />
        <TextBox
            type="text"
            id="emailAddress"
            label='Valid email address'
            // placeholder="Valid email address"
            onValueChanged={(e) => setEmail(e.value)}
            value={email}
            height={40}
            style={{ fontSize: "12px" }}
            className=" border pl-1 w-full md:w-1/2 lg:w-[60%] xl:w-[80%] outline-none "
          />
        <TextBox
            type="text"
            id="country"
            label='Country of origin'
            // placeholder="Country of origin"
            onValueChanged={(e) => setCountry(e.value)}
            value={country}
            height={40}
            style={{ fontSize: "12px" }}
            className=" border pl-1 w-full md:w-1/2 lg:w-[60%] xl:w-[80%] outline-none "
          />
        <TextBox
            type="text"
            id="telephone"
            label='Telephone number'
            // placeholder="Telephone number"
            onValueChanged={(e) => setTelephone(e.value)}
            value={telephone}
            height={40}
            style={{ fontSize: "12px" }}
            className=" border pl-1 w-full md:w-1/2 lg:w-[60%] xl:w-[80%] outline-none "
          />
          <TextArea
            type="text"
            height="150px"
            label='Extra notes about the user'
            id="extras"
            onValueChanged={(e) => setExtras(e.value)}
            value={extras}
            style={{ fontSize: "12px" }}
            className=" border resize-none text-xs pl-1 w-full md:w-[70%] lg:w-[80%] outline-none"
          />

          <Button text='Invite user' onClick={handleSubmit} height={40} width={200} icon=''/>
      </div>
      {modalView && (
      <div className='absolute top-1/3 w-1/3 p-6 h-max rounded bg-white shadow-md z-50 flex flex-col gap-2'>
        <ConfirmationSuccess handleModalReset={handleModalReset} handleDashboard={handleDashboard} />
      </div>  
      )}
    </div>
  )
}

export default InviteUsers;