import React from 'react';
import { Card, CardProps } from 'antd';

function StyledCard(props: CardProps) {
  const { children } = props;

  return <Card {...props}>{children}</Card>;
}

export default StyledCard;
