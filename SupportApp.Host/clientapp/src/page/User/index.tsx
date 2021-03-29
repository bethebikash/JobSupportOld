import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchData } from 'redux/actions/usersAction';
import { RootState } from 'redux/store';
import { Badge, Col, Row } from 'antd';

import StyledCard from 'components/card';
import LayoutOne from 'components/layout/LayoutOne';

interface IUserProps {
  title: string;
  body: string;
  id: number;
}

function User() {
  const dispatch = useDispatch();
  const users = useSelector((state: RootState) => state.users);

  useEffect(() => {
    dispatch(fetchData('posts'));
  }, [dispatch]);

  return (
    <LayoutOne>
      <div style={{ padding: '40px' }}>
        <Row gutter={[20, 20]}>
          {users.user.length > 0 &&
            users.user.map((user: IUserProps) => (
              <Col key={user.id} sm={6}>
                <StyledCard title={user.title} loading={users.loading}>
                  {user.body}
                </StyledCard>
              </Col>
            ))}
          {users.loading && <Badge status="processing">Loading...</Badge>}
        </Row>
      </div>
    </LayoutOne>
  );
}

export default User;
