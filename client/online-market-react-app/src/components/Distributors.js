import React, { useState, useEffect } from 'react';
import { Table, Button, Modal, Form, Input, DatePicker, Select, Popconfirm, Space, Tabs, Upload, Image } from 'antd';
import { privateApi } from '../api/axios';
import TabPane from 'antd/es/tabs/TabPane';
import { UploadOutlined } from "@ant-design/icons";
import dayjs from "dayjs";


const { Option } = Select;

const Distributors = () => {
  const [dataSource, setDataSource] = useState([]);
  const [recommenders, setRecommenders] = useState([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isContactModal, setIsContactModal] = useState(false);
  const [isAddressModal, setIsAddressModal] = useState(false);
  const [isDocumentModal, setIsDocumentModal] = useState(false);



  const [contacts, setContacts] = useState([]);
  const [documents, setDocuments] = useState([]);
  const [addresses, setAddresses] = useState([]);

  const [imageFile, setImageFile] = useState(null);

  const [form] = Form.useForm();
  const [contactForm] = Form.useForm();
  const [documentForm] = Form.useForm();
  const [addressForm] = Form.useForm();



  useEffect(() => {
    fetchData();

  }, []);

  const contactTypes = {
    0: 'TelePhone',
    1: 'MobilePhone',
    2: 'Email',
    3: 'Fax'

  }


  const addressType = {
    0: 'Registration',
    1: 'Actual',
  }

  const documentType = {
    0: 'Passport',
    1: 'ID'
  }



  function randomIntFromInterval(min, max) { // min and max included 
    return Math.floor(Math.random() * (max - min + 1) + min)
  }




  const fetchData = async () => {
    try {
      const response = await privateApi.get('/api/distributor/get_all_distributor'); // Replace with your API endpoint
      if (response.status === 200) {
        setDataSource(response.data);

      }
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const fetchRecommenders = async (id) => {
    try {
      const response = await privateApi.get('/api/distributor/get_all_posible_recomendator'); // Replace with your API endpoint
      setRecommenders(response.data.filter(f => f.id !== id));
    } catch (error) {
      console.error('Error fetching recommenders:', error);
    }
  };
  const normFile = (e) => {
    console.log('Upload event:', e);
    if (Array.isArray(e)) {
      return e;
    }
    return e?.fileList;
  };

  const imageUploadHandler = ({ fileList }) => {

    setImageFile(fileList);
  };

  const columns = [
    {
      title: "image",
      dataIndex: "img_path",
      key: "img_path",
      render: (record) => (
        <Image
          width={150}
          src={"https://localhost:7012/Images/" + record}
        />
      ),
    },
    {
      title: 'ID',
      dataIndex: 'id',
      key: 'id',
    },
    {
      title: 'First Name',
      dataIndex: 'first_name',
      key: 'first_name',
    },
    {
      title: 'Last Name',
      dataIndex: 'last_name',
      key: 'last_name',
    },
    {
      title: 'Birth Date',
      dataIndex: 'birth_date',
      key: 'birth_date',
      render: (text) => {
        return new Date(text).toLocaleDateString();
      },
    },
    {
      title: 'Recommender',
      dataIndex: 'recommender',
      key: 'recommender',
      render: (recommenderId) => {
        const recommender = dataSource.find((r) => r.id === recommenderId);
        return recommender ? recommender.first_name + " " + recommender.last_name : '';
      },
    },
    {
      title: 'Action',
      key: 'action',
      render: (record) => (
        <span>
          <Button type="link" onClick={() => handleEdit(record)}>
            Edit
          </Button>
          <Popconfirm title="Sure to delete?" onConfirm={() => handleRemove(record.id)}>
            {/* <a>Delete</a> */}
            <Button type="link">
              Delete
            </Button>
          </Popconfirm>
        </span>
      ),
    },
  ];

  const renderContactsTable = () => {
    const columns = [

      {
        title: 'Type',
        dataIndex: 'type',
        key: 'type',
        render: (type) => {
          return contactTypes[type]
        }
      },
      {
        title: 'Contact info',
        dataIndex: 'contact_info',
        key: 'contact_info',
      },
      {
        title: 'Action',
        key: 'action',
        render: (text, record) => (
          <span>
            <Button type="link" onClick={() => editContact(record)}>
              Edit
            </Button>
            <Popconfirm title="Sure to delete?" onConfirm={() => contactRemove(record.id)}>
              {/* <a>Delete</a> */}
              <Button type="link">
                Delete
              </Button>
            </Popconfirm>
          </span>
        ),
      }
    ];

    return <Table dataSource={contacts} columns={columns} />;
  };

  const renderDocumentsTable = () => {
    const columns = [
      {
        title: 'Type',
        dataIndex: 'type',
        key: 'type',
        render: (type) => documentType[type]
      },
      {
        title: 'Document seria',
        dataIndex: 'document_seria',
        key: 'document_seria',
      },

      {
        title: 'Document number',
        dataIndex: 'document_number',
        key: 'document_number',
      },
      {
        title: 'Personal number',
        dataIndex: 'personal_number',
        key: 'personal_number',
      },
      {
        title: 'Release date',
        dataIndex: 'release_date',
        key: 'release_date',
        render: (release_date) => new Date(release_date).toLocaleDateString()
      },
      {
        title: 'Due date',
        dataIndex: 'due_date',
        key: 'due_date',
        render: (due_date) => new Date(due_date).toLocaleDateString()

      },
      {
        title: 'Document seria',
        dataIndex: 'document_seria',
        key: 'document_seria',
      },
      {
        title: 'Document seria',
        dataIndex: 'document_seria',
        key: 'document_seria',
      },

      {
        title: 'Action',
        key: 'action',
        render: (text, record) => (
          <span>
            <Button type="link" onClick={() => editDocument(record)}>
              Edit
            </Button>
            <Popconfirm title="Sure to delete?" onConfirm={() => documentRemove(record.id)}>
              {/* <a>Delete</a> */}
              <Button type="link">
                Delete
              </Button>
            </Popconfirm>
          </span>
        ),
      }

    ];

    return <Table dataSource={documents} columns={columns} />;
  };

  const renderAddressesTable = () => {
    const columns = [

      {
        title: 'Type',
        dataIndex: 'type',
        key: 'type',
        render: (type) => {
          return addressType[type]
        }
      },
      {
        title: 'Address',
        dataIndex: 'address',
        key: 'address',
      }, {
        title: 'Action',
        key: 'action',
        render: (record) => (
          <span>
            <Button type="link" onClick={() => editAddress(record)}>
              Edit
            </Button>
            <Popconfirm title="Sure to delete?" onConfirm={() => addressRemove(record.id)}>
              <Button type="link">
                Delete
              </Button>
            </Popconfirm>
          </span>
        ),
      }


    ];

    return <Table dataSource={addresses} columns={columns} />;
  };


  const handleAdd = () => {
    form.resetFields();
    setDocuments([])
    setAddresses([])
    setContacts([])
    setIsModalVisible(true);
    fetchRecommenders();

  };

  const handleAddDocuments = () => {
    documentForm.resetFields();

    setIsDocumentModal(true);
  };

  const handleAddContact = () => {
    contactForm.resetFields();

    setIsContactModal(true);
  };

  const handleAddAddress = () => {
    addressForm.resetFields();

    setIsAddressModal(true);
  };

  // let img_path = undefined

  const handleEdit = async (record) => {
    fetchRecommenders(record.id);

    const response = await privateApi.get('/api/distributor/get_distributor_by_id', { params: { id: record.id } })
    if (response.status === 200) {
      const forForm = {
        ...response.data,
        birth_date: dayjs(response.data.birth_date)
      }

      form.setFieldsValue(forForm);
      setDocuments(response.data.documents)
      setAddresses(response.data.addresses)
      setContacts(response.data.contacts)
      setIsModalVisible(true);
    }
  };

  const handleRemove = async (id) => {
    try {
      const response = await privateApi.post('/api/distributor/remove_distributor', { id: id, name: '' });
      if (response.status === 200) {
        fetchData()
      }
    } catch (error) {
      console.error('Error deleting distributor:', error);
    }
  };

  const handleOk = () => {
    form.validateFields().then(async (values) => {
      try {

        if (imageFile)
          values.img_byte = imageFile[0].thumbUrl.split(",")[1];

        if (values.id) {
          // Edit existing record
          if (contacts.length > 0) {
            contacts.forEach(f => { if (f.id < 0) { f.id = 0 } })
          }
          if (documents.length > 0) {
            documents.forEach(f => { if (f.id < 0) { f.id = 0 } })
          }
          if (addresses.length > 0) {
            addresses.forEach(f => { if (f.id < 0) { f.id = 0 } })
          }

          const formModel = { ...values, documents: documents, contacts: contacts, addresses: addresses }
          console.log(formModel);
          const response = await privateApi.put('/api/distributor/update_distributor', formModel);
          if (response.status === 200) {
            fetchData()
            setIsModalVisible(false);

          }
          else {
            console.log(response)
            alert(response.data.message)
          }

        } else {

          if (contacts.length > 0) {
            contacts.forEach(f => { if (f.id < 0) { f.id = 0 } })
          }
          if (documents.length > 0) {
            documents.forEach(f => { if (f.id < 0) { f.id = 0 } })
          }
          if (addresses.length > 0) {
            addresses.forEach(f => { if (f.id < 0) { f.id = 0 } })
          }
          const formModel = { ...values, documents, contacts, addresses }
          const response = await privateApi.post('/api/distributor/add_distributor', formModel);
          if (response.status === 200) {
            fetchData()
            setIsModalVisible(false);
          }
          else {
            console.log(response)
            alert(response.data.message)
          }
        }

      } catch (error) {
        console.error('Error saving distributor:', error);
      }
    }).catch((error) => {
      console.log(error)
    });
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  const cancelContact = () => {
    setIsContactModal(false);
  };




  const okContact = () => {
    contactForm.validateFields().then(async (values) => {
      try {

        if (values.id) {

          setContacts((prevData) =>
            prevData.map((item) => (item.id === values.id ? { ...item, ...values } : item))
          );
        } else {
          // Add new record
          // Make an API call to add the new distributor on the server
          console.log(new Date().getMilliseconds());
          values.id = randomIntFromInterval(-999999, -999)
          setContacts((prevData) => [...prevData, { ...values }]);
        }

        setIsContactModal(false);
      } catch (error) {
        console.error('Error saving distributor:', error);
      }
    }).catch(error => {
      console.log(error)
    });
  }
  const editContact = async (record) => {

    if (record) {
      const forForm = {
        ...record,
      }
      contactForm.setFieldsValue(forForm);

      setIsContactModal(true);
    }
  };

  const contactRemove = async (id) => {
    try {
      // Make an API call to delete the distributor on the server

      setContacts((prevData) => prevData.filter((item) => item.id !== id));
    } catch (error) {
      console.error('Error deleting distributor:', error);
    }
  };


  const editDocument = async (record) => {

    if (record) {
      console.log(dayjs(record.due_date))
      const forForm = {

        ...record,
        due_date: dayjs(record.due_date),
        release_date: dayjs(record.release_date),
      }
      documentForm.setFieldsValue(forForm);

      setIsDocumentModal(true);
    }
  };

  const documentRemove = async (id) => {
    try {
      // Make an API call to delete the distributor on the server

      setDocuments((prevData) => prevData.filter((item) => item.id !== id));
    } catch (error) {
      console.error('Error deleting documents:', error);
    }
  };



  const editAddress = async (record) => {
    if (record) {
      addressForm.setFieldsValue(record);
      setIsAddressModal(true);
    }
  };

  const addressRemove = async (id) => {
    try {
      setAddresses((prevData) => prevData.filter((item) => item.id !== id));
    } catch (error) {
      console.error('Error deleting documents:', error);
    }
  };


  const okAddress = () => {
    addressForm.validateFields().then(async (values) => {
      try {
        if (values.id) {
          setAddresses((prevData) =>
            prevData.map((item) => (item.id === values.id ? { ...item, ...values } : item))
          );
        } else {
          values.id = randomIntFromInterval(-999999, -999)
          setAddresses((prevData) => [...prevData, { ...values }]);
        }
        setIsAddressModal(false);
      } catch (error) {
        console.error('Error saving address:', error);
      }
    }).catch(error => {
      console.log(error)
    });
  }


  const cancelAddress = () => {
    addressForm.resetFields()
    setIsAddressModal(false)
  }

  const okDocument = () => {
    documentForm.validateFields().then(async (values) => {
      try {

        if (values.id) {
          setDocuments((prevData) =>
            prevData.map((item) => (item.id === values.id ? { ...item, ...values } : item))
          );
        } else {
          console.log(new Date().getMilliseconds());
          values.id = randomIntFromInterval(-999999, -999)
          setDocuments((prevData) => [...prevData, { ...values }]);
        }

        setIsDocumentModal(false);
      } catch (error) {
        console.error('Error saving document:', error);
      }
    }).catch(error => {
      console.log(error)
    });
  }



  const cancelDocument = () => {
    documentForm.resetFields()
    setIsDocumentModal(false)
  }

  return (
    <div>
      <h2 style={{ textAlign: 'center' }}>Distributors Page</h2>
      <Space style={{ margin: 16 }}>
        <Button type="primary" onClick={handleAdd}>
          Add Distributor
        </Button>
      </Space>
      <Table dataSource={dataSource} columns={columns} />

      <Modal title="Add/Edit Distributor" open={isModalVisible} width={1200} onOk={handleOk} onCancel={handleCancel}>
        <Form form={form} layout="vertical">
          <Tabs defaultActiveKey="1" tabPosition="left">
            <TabPane tab="Main" key="1">
              <Form name="img_path" >
                <Image />
              </Form>
              <Form.Item name="id" hidden>
                <Input />
              </Form.Item>

              <Form.Item label="First Name" name="first_name" rules={[{ required: true, message: 'Please enter First Name!' }]}>
                <Input />
              </Form.Item>
              <Form.Item label="Last Name" name="last_name" rules={[{ required: true, message: 'Please enter Last Name!' }]}>
                <Input />
              </Form.Item>
              <Form.Item label="Birth Date" name="birth_date" rules={[{ required: true, message: 'Please enter Birth Date!' }]}>
                <DatePicker />
              </Form.Item>
              <Form.Item label="Recommender" name="recommender">
                <Select placeholder="Select recommender" allowClear>
                  {recommenders.map((recommender) => (
                    <Option key={recommender.id} value={recommender.id}>
                      {recommender.name}
                    </Option>
                  ))}
                </Select>
              </Form.Item>

              <Form.Item label="Gender" name="gender" rules={[{ required: true, message: 'Please select gendere!' }]}>
                <Select placeholder="Select gender">
                  <Option key={0} value={0}>
                    Male
                  </Option>
                  <Option key={1} value={1}>
                    FeMale
                  </Option>
                </Select>
              </Form.Item>

              <Form.Item
                name="upload"
                label="Upload"
                valuePropName="fileList"
                getValueFromEvent={normFile}      >
                <Upload name="logo" listType="picture" onChange={imageUploadHandler}>
                  <Button icon={<UploadOutlined />}>Click to upload</Button>
                </Upload>

              </Form.Item>

            </TabPane>
            <TabPane tab="Contacts" key="2">
              <Space style={{ margin: 10 }}>
                <Button type="primary" onClick={handleAddContact}>
                  Add Contact
                </Button>
              </Space>
              {/* Contacts Table */}
              {renderContactsTable()}
            </TabPane>
            <TabPane tab="Documents" key="3">
              <Space style={{ margin: 10 }}>
                <Button type="primary" onClick={handleAddDocuments}>
                  Add Documents
                </Button>
              </Space>
              {/* Documents Table */}
              {renderDocumentsTable()}
            </TabPane>
            <TabPane tab="Addresses" key="4">
              <Space style={{ margin: 10 }}>
                <Button type="primary" onClick={handleAddAddress}>
                  Add Address
                </Button>
              </Space>
              {/* Addresses Table */}
              {renderAddressesTable()}
            </TabPane>
          </Tabs>
        </Form>
      </Modal>



      <Modal title="Add/Edit Contact" open={isContactModal} onOk={okContact} onCancel={cancelContact}>
        <Form form={contactForm}
          layout="vertical"

        >
          <Form.Item name="id" hidden>
            <Input />
          </Form.Item>



          <Form.Item label="Contact Type" name="type" rules={[{ required: true, message: 'Please select Contact Type!' }]}>
            <Select placeholder="Select contact type">
              <Option value={0}>TelePhone</Option>
              <Option value={1}>MobilePhone</Option>
              <Option value={2}>Email</Option>
              <Option value={3}>Fax</Option>


              {/* Add more options if needed */}
            </Select>
          </Form.Item>
          <Form.Item label="Contact Information" name="contact_info" rules={[{ required: true, message: 'Please enter Contact Information!' }]}>
            <Input />
          </Form.Item>
        </Form>
      </Modal>

      <Modal title="Add/Edit Address" open={isAddressModal} onOk={okAddress} onCancel={cancelAddress}>
        <Form form={addressForm}
          layout="vertical"

        > <Form.Item name="id" hidden>
            <Input />
          </Form.Item>


          <Form.Item label="Contact Type" name="type" rules={[{ required: true, message: 'Please select Contact Type!' }]}>
            <Select placeholder="Select contact type">
              <Option value={0}>Registration</Option>
              <Option value={1}>Actual</Option>
            </Select>
          </Form.Item>
          <Form.Item label="Address " name="address" rules={[{ required: true, message: 'Please enter Address Information!' }]}>
            <Input />
          </Form.Item>
        </Form>
      </Modal>


      <Modal title="Add/Edit Documents" open={isDocumentModal} onOk={okDocument} onCancel={cancelDocument}>
        <Form form={documentForm}
          layout="vertical"

        >
          <Form.Item name="id" hidden>
            <Input />
          </Form.Item>


          <Form.Item label="Contact Type" name="type" rules={[{ required: true, message: 'Please select document Type!' }]}>
            <Select placeholder="Select document type">
              <Option value={0}>Passport</Option>
              <Option value={1}>PersonalId</Option>
            </Select>
          </Form.Item>
          <Form.Item label="Document seria " name="document_seria" rules={[{ required: true, message: 'Please enter Document seria!' }]}>
            <Input />
          </Form.Item>
          <Form.Item label="Document number " name="document_number" rules={[{ required: true, message: 'Please enter Document number!' }]}>
            <Input />
          </Form.Item>
          <Form.Item label="Release Date" name="release_date" rules={[{ required: true, message: 'Please enter Release Date!' }]}>
            <DatePicker />
          </Form.Item>
          <Form.Item label="Due Date" name="due_date" rules={[{ required: true, message: 'Please enter Due Date!' }]}>
            <DatePicker />
          </Form.Item>
          <Form.Item label="Personal number" name="personal_number" rules={[{ required: true, message: 'Please enter Personal number!' }]}>
            <Input />
          </Form.Item>
          <Form.Item label="Agency " name="agency">
            <Input />
          </Form.Item>
        </Form>
      </Modal>



    </div>
  );
};

export default Distributors;
