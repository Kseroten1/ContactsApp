function Contact(props) {
  //   const contact = {
  //     id: id,
  //     firstName: firstName,
  //     lastName: lastName,
  //     phoneNumber: phoneNumber,
  //     email: email,
  //     dateOfBirth: dateOfBirth,
  //     categoryId: categoryId,
  //     categoryName: categoryName,
  //     subCategoryId: subCategoryId,
  //     subCategoryname: subCategoryName,
  //   };
  const contact = props.contact;
  console.log(props);
  return (
    <div>
      {contact.contactId} <span>{contact.firstName} </span>
      <span>{contact.lastName} </span>
      <span>{contact.category} </span>
    </div>
  );
}

export default Contact;
