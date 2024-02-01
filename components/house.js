import { useContext } from 'react';
import currencyFormatter from '../helpers/currencyFormatter';
import defaultPhoto from '../helpers/defaultPhoto';
import { navigationContext } from './app';
import navValues from '@/helpers/navValues';

const House = () => {
  const { navigate, param: house } = useContext(navigationContext);
  return (
    <>
      <div className="row">
        <div className="col-6">
          <div className="row">
            <img
              className="img-fluid"
              src={
                house.photo ? `./houseImages/${house.photo}.jpeg` : defaultPhoto
              }
              alt="House pic"
            />
          </div>
        </div>
        <div className="col-6">
          <div className="row mt-2">
            <h5 className="col-12">{house.country}</h5>
          </div>
          <div className="row">
            <h3 className="col-12">{house.address}</h3>
          </div>
          <div className="row">
            <h2 className="themeFontColor col-12">
              {currencyFormatter.format(house.price)}
            </h2>
          </div>
          <div className="row">
            <div className="col-12 mt-3">{house.description}</div>
          </div>
        </div>
      </div>
      <button
        className="btn btn-primary mt-4"
        onClick={() => navigate(navValues.home)}
      >
        Go back
      </button>
    </>
  );
};

export default House;
