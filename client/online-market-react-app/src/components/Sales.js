// src/components/Sales.js
import React, { useState, useEffect } from 'react';
import { Table, Button, Modal, Form, Input, Space, DatePicker, Select } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
import { privateApi } from '../api/axios';
import dayjs from "dayjs";

const { Option } = Select;

const Sales = () => {
    const [dataSource, setDataSource] = useState([]);
    const [distributorOptions, setDistributorOptions] = useState([]);
    const [productOptions, setProductOptions] = useState([]);
    const [productsList, setProductsList] = useState([]);
    const [filteredDataSource, setFilteredDataSource] = useState([]);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [form] = Form.useForm();

    useEffect(() => {
        // Fetch data from API when the component mounts
        fetchData();
        fetchDistributors();
        fetchProducts();
    }, []);

    const fetchData = async () => {
        try {
            const response = await privateApi.get('/api/sale/get_all_sales'); // Replace with your API endpoint
            setDataSource(response.data);
            setFilteredDataSource(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    const fetchDistributors = async () => {
        try {
            const response = await privateApi.get('/api/distributor/get_distributors_for_selection'); // Replace with your API endpoint
            console.log(response)
            setDistributorOptions(response.data);
        } catch (error) {
            console.error('Error fetching distributors:', error);
        }
    };

    const fetchProducts = async () => {
        try {
            const response = await privateApi.get('/api/product/get_all_products'); // Replace with your API endpoint
            setProductOptions(response.data);
        } catch (error) {
            console.error('Error fetching products:', error);
        }
    };

    const columns = [
        {
            title: 'ID',
            dataIndex: 'id',
            key: 'id',
        },
        {
            title: 'Distributor ID',
            dataIndex: 'distributor_id',
            key: 'distributor_id',
            hidden: true
        },
        {
            title: 'Distributor ',
            dataIndex: 'distributor_name',
            key: 'distributor_name',
        },
        {
            title: 'Transaction Date',
            dataIndex: 'tdate',
            key: 'tdate',
            render: (text) => {
                return new Date(text).toLocaleDateString();
            },
        },
        {
            title: 'Total Price',
            dataIndex: 'total',
            key: 'total',
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

    const productColumns = [
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
            title: 'Sale Price',
            dataIndex: 'product_price',
            key: 'product_price',
        },
        {
            title: 'Unit Price',
            dataIndex: 'product_self_cost',
            key: 'product_self_cost',
        },
    ];

    const handleAddProduct = () => {
        const selectedProductId = form.getFieldValue('selectedProduct');
        const selectedProduct = productOptions.find((product) => product.id === selectedProductId);


        if (selectedProduct) {
            setProductsList((prevList) => [
                ...prevList,
                {
                    id: 0,
                    sale_id: 0,
                    product_id: selectedProduct.id,
                    product_price: selectedProduct.unit_price,
                    product_self_cost: selectedProduct.unit_price,
                    code: selectedProduct.code,
                    name: selectedProduct.name
                },
            ]);
        }

        // Clear the selected product in the form
        form.setFieldsValue({ selectedProduct: undefined });
    };

    const handleRemoveProduct = (productId) => {
        setProductsList((prevList) => prevList.filter((product) => product.product_id !== productId));
    };

    const handleAdd = () => {
        form.resetFields();
        setProductsList([]);
        console.log(distributorOptions)

        // Clear the products list when adding a new sale
        setIsModalVisible(true);
    };

    const handleEdit = async (record) => {
        const response = await privateApi.get('/api/sale/get_sale_by_id', { params: { id: record.id } })
        if (response.status === 200) {
            console.log(response)
            const formModel = { ...response.data, tdate: dayjs(response.data.tdate) }
            console.log(formModel)
            setProductsList(formModel.products);
            form.setFieldsValue(formModel);
        }
        else {
            console.log(response)
        }

        setIsModalVisible(true);
    };

    const handleRemove = async (id) => {
        const response = await privateApi.post("/api/sale/remove_sale", { id: id, name: '' })
        if (response.status === 200)
          fetchData()
        else {
          console.log(response)
        }
      };

    const handleOk = () => {
        form.validateFields().then(async (values) => {
            try {


                if (values.id) {

                    const formModel = { ...values,products:productsList }
                    console.log(formModel);
                    const response = await privateApi.put('/api/sale/update_sale', formModel);
                    if (response.status === 200) {
                        fetchData()
                        setIsModalVisible(false);

                    }
                    else {
                        console.log(response)
                        alert(response.data.message)
                    }

                } else {


                    const formModel = { ...values,products:productsList }
                    const response = await privateApi.post('/api/sale/add_sale', formModel);
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
                console.error('Error saving sale:', error);
            }
        }).catch((error) => {
            console.log(error)
        });
    };


    // const handleOk = () => {
    //     form.validateFields().then(async (values) => {
    //         values.products = productsList;

    //         if (form.getFieldValue('id')) {

    //             setDataSource((prevData) =>
    //                 prevData.map((item) => (item.id === values.id ? { ...item, ...values } : item))
    //             );
    //             setFilteredDataSource((prevData) =>
    //                 prevData.map((item) => (item.id === values.id ? { ...item, ...values } : item))
    //             );
    //         } else {
    //             // Add new record
    //             // You can make an API call to add the new sale on the server as well
    //             setDataSource((prevData) => [...prevData, { ...values }]);
    //             setFilteredDataSource((prevData) => [...prevData, { ...values }]);
    //         }

    //         setIsModalVisible(false);
    //     });
    // };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    const handleSearch = (value) => {
        const filteredData = dataSource.filter(
            (item) =>
                String(item.distributor_id).includes(value) ||
                new Date(item.tdate).toLocaleDateString().includes(value) ||
                String(item.total).includes(value)
        );
        setFilteredDataSource(filteredData);
    };

    return (
        <div>
            <h2 style={{ textAlign: 'center' }}>Sales Page</h2>
            <Space style={{ margin: 16 }}>
                <Button type="primary" onClick={handleAdd}>
                    Add Sale
                </Button>
                <Input
                    placeholder="Search..."
                    prefix={<SearchOutlined />}
                    onChange={(e) => handleSearch(e.target.value)}
                />
            </Space>

            <Table dataSource={filteredDataSource} columns={columns} />

            <Modal title="Add/Edit Sale" open={isModalVisible} onOk={handleOk} onCancel={handleCancel}>
                <Form form={form} layout="vertical">
                    <Form.Item name="id" hidden>
                        <Input />
                    </Form.Item>
                    <Form.Item label="Distributor ID" name="distributor_id" rules={[{ required: true, message: 'Please select Distributor ID!' }]}>
                        <Select
                            showSearch
                            placeholder="Select a distributor"
                            optionFilterProp="children"
                            style={{ width: '100%', marginBottom: 16 }}
                            allowClear
                        >
                            {distributorOptions.map((distributor) => (
                                <Option key={distributor.id} value={distributor.id}>
                                    {distributor.name}
                                </Option>
                            ))}
                        </Select>
                    </Form.Item>
                    <Form.Item label="Transaction Date" name="tdate" rules={[{ required: true, message: 'Please enter Transaction Date!' }]}>
                        <DatePicker />
                    </Form.Item>


                    <Form.Item label="Total Price" name="total" hidden>
                        <Input />
                    </Form.Item>

                    <h3>Products</h3>
                    <Select
                        showSearch
                        placeholder="Select a product"
                        optionFilterProp="children"
                        style={{ width: '100%', marginBottom: 16 }}
                        allowClear
                        onSelect={(value) => form.setFieldsValue({ selectedProduct: value })}
                    >
                        {productOptions.map((product) => (
                            <Option key={product.id} value={product.id}>
                                {product.name}
                            </Option>
                        ))}
                    </Select>
                    <Button type="primary" onClick={handleAddProduct} style={{ marginBottom: 16 }}>
                        Add Product
                    </Button>
                    <Table
                        dataSource={productsList}
                        columns={[
                            ...productColumns,
                            {
                                title: 'Action',
                                key: 'action',
                                render: (text, record) => (
                                    <Button type="link" onClick={() => handleRemoveProduct(record.product_id)}>
                                        Remove
                                    </Button>
                                ),
                            },
                        ]}
                    />

                </Form>
            </Modal>
        </div>
    );
};

export default Sales;
