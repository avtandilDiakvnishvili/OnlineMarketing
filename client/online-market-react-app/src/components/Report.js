import React, { useState, useEffect } from 'react';
import { Table, DatePicker, Button } from 'antd';
import { privateApi } from '../api/axios';

const { RangePicker } = DatePicker;

const Reports = () => {
    const [reportData, setReportData] = useState([]);
    const [dateRange, setDateRange] = useState([]);
    const [distributorsFilter, setDistributorFilter] = useState([]);

    useEffect(() => {
        fetchReportData();
    }, [dateRange]); // Re-fetch data when date range changes

    const fetchReportData = async () => {
        try {
            // Replace the URL with your actual API endpoint for fetching reports
            const filter = {
                start_date: dateRange[0] ? dateRange[0].format('YYYY-MM-DD') : undefined,
                end_date: dateRange[1] ? dateRange[1].format('YYYY-MM-DD') : undefined,
            }
            console.log(filter)
            const response = await privateApi.post('/api/distributor/get_distributors_bonus', filter);

            const distColumnFilter = response.data.map(f => { return { text: f.name, value: f.name } })
            const arrayUniqueByKey = [...new Map(distColumnFilter.map(item =>
                [item['text'], item])).values()];
            console.log(arrayUniqueByKey)
            setDistributorFilter(arrayUniqueByKey)

            setReportData(response.data);

        } catch (error) {
            console.error('Error fetching report data:', error);
        }
    };

    const caclulateBonus = async () => {
        try {
            // Replace the URL with your actual API endpoint for fetching reports
            const filter = {
                start_date: dateRange[0] ? dateRange[0].format('YYYY-MM-DD') : undefined,
                end_date: dateRange[1] ? dateRange[1].format('YYYY-MM-DD') : undefined,
            }
            console.log(filter)
            const response = await privateApi.post('/api/distributor/calculate_distributor_bonus', filter);
            if (response.status === 200)
                fetchReportData();
        } catch (error) {
            console.error('Error calculate bonus :', error);
        }
    }
    const columns = [

        {
            title: 'Distributor',
            dataIndex: 'name',
            key: 'name',
            sorter: (a, b) => a.name.localeCompare(b.name),
            filters: distributorsFilter,
            onFilter: (value, record) => record.name === value,
        },
        {
            title: 'Bonus',
            dataIndex: 'bonus',
            key: 'bonus',
            sorter: (a, b) => a.bonus - b.bonus,

        },
        {
            title: 'Start date',
            dataIndex: 'start_date',
            key: 'start_date',
            sorter: (a, b) => new Date(a.start_date) - new Date(b.start_date),
            render:(start_date)=>new Date(start_date).toLocaleDateString()

        },
        {
            title: 'End date',
            dataIndex: 'end_date',
            key: 'end_date',
            sorter: (a, b) => new Date(a.end_date) - new Date(b.end_date),
            render:(end_date)=>new Date(end_date).toLocaleDateString()


        },
    ];

    const handleDateRangeChange = (dates) => {
        setDateRange(dates);
    };

    const handleFilterClick = () => {
        fetchReportData([]);
    };

    return (
        <div>
            <h2>Reports Page</h2>
            <div style={{ marginBottom: '16px' }}>
                <RangePicker onChange={handleDateRangeChange} />
                <Button type="primary" onClick={handleFilterClick} style={{ marginLeft: '8px' }}>
                    Apply Filters
                </Button>
                <Button type="primary" onClick={caclulateBonus} style={{ marginLeft: '8px' }}>
                    Calculate Bonuses
                </Button>
            </div>
            <Table dataSource={reportData} columns={columns} />
        </div>
    );
};

export default Reports;

