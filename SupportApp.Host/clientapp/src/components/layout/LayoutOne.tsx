import React, { ReactNode } from 'react';
import Layout, { Content } from 'antd/lib/layout/layout';
import Title from 'antd/lib/typography/Title';

import CustomMenu from './CustomMenu';
import { StyledHeader } from './styled';

interface ILayoutProps {
  children: ReactNode | any;
}

function LayoutOne(props: ILayoutProps) {
  const { children } = props;

  return (
    <Layout>
      <StyledHeader>
        <Title>Logo</Title>
        <CustomMenu />
      </StyledHeader>
      <Content>{children}</Content>
    </Layout>
  );
}

export default LayoutOne;
