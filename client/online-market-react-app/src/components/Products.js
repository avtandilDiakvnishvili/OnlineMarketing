// src/components/Products.js
import React, { useState, useEffect } from 'react';
import { Table, Button, Modal, Form, Input, Space } from 'antd';
import { privateApi } from '../api/axios';

const Products = () => {
  const [dataSource, setDataSource] = useState([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [form] = Form.useForm();

  useEffect(() => {
    // Fetch data from API when the component mounts
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await privateApi.get('/api/product/get_all_products'); // Replace with your API endpoint
      setDataSource(response.data);
      console.log(response)
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const columns = [
    {
      title: 'ID',
      dataIndex: 'id',
      key: 'id',
    },
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Code',
      dataIndex: 'code',
      key: 'code',
    },
    {
      title: 'Price',
      dataIndex: 'unit_price',
      key: 'unit_price',
    },
    {
      title: 'Action',
      key: 'action',
      render: (text, record) => (
        <span>
          <Button type="link" onClick={() => handleEdit(record)}>
            Edit
          </Button>
          <Button type="link" onClick={() => handleRemove(record.id)}>
            Remove
          </Button>
        </span>
      ),
    },
  ];

  const handleAdd = () => {
    form.resetFields();
    setIsModalVisible(true);
  };

  const handleEdit = (record) => {
    form.setFieldsValue(record);
    setIsModalVisible(true);
  };

  const handleRemove = async (id) => {
    const response = await privateApi.post("/api/product/remove_product", { id: id, name: '' })
    if (response.status === 200)
      fetchData()
    else {
      console.log(response)
    }
  };

  const handleOk = () => {
    form.validateFields().then(async (values) => {
      if (form.getFieldValue('id')) {
        // Edit existing record
        // You can make an API call to update the product on the server as well
        const updateModel = { ...values };
        const response = await privateApi.put('/api/product/update_product', updateModel); // Replace with your API endpoint
        if (response.status === 200)
          fetchData();
        else {
          console.log(response)
        }
        // setDataSource((prevData) =>
        //   prevData.map((item) => (item.id === values.id ? { ...item, ...values } : item))
        // );
      } else {
        // Add new record
        // You can make an API call to add the new product on the server as well
        const model = { id: 0, ...values };
        console.log(model)
        const response = await privateApi.post('/api/product/add_product', model); // Replace with your API endpoint
        if (response.status === 200)
          fetchData();
        else {
          console.log(response)
        }
      }
      setIsModalVisible(false);
    }).catch(async (error) => {
      console.log(error)
    })
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  return (
    <div>
      <h2 style={{textAlign:'center'}}>Products Page</h2>
      <Space style={{ margin: 16 }}>
        <Button type="primary" onClick={handleAdd}>
          Add Product
        </Button>

      </Space>
      <Table dataSource={dataSource} columns={columns} key="table" />

      <Modal title="Add/Edit Product" open={isModalVisible} onOk={handleOk} onCancel={handleCancel}>
        <Form form={form} layout="verztical">
          <Form.Item name="id" hidden>
            <Input />
          </Form.Item>
          <Form.Item label="Name" name="name" rules={[{ required: true, message: 'Please enter Name!' }]}>
            <Input />
          </Form.Item>
          <Form.Item label="Code" name="code" rules={[{ required: true, message: 'Please enter Code!' }]}>
            <Input />
          </Form.Item>
          <Form.Item label="Price" name="unit_price" rules={[{ required: true, message: 'Please enter Price!' }]}>
            <Input type="number" />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default Products;
