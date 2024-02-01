import { useEffect, useState } from 'react';
import HouseRow from './houseRow';

const HouseList = () => {
  const [houses, setHouses] = useState([]);

  useEffect(() => {
    const fetchHouses = async () => {
      const response = await fetch('/api/houses');
      const houses = await response.json();
      setHouses(houses);
    };
    fetchHouses();
  }, []);

  const addHouse = () => {
    setHouses([
      ...houses,
      {
        id: 3,
        address: '32 Valley Way, New York',
        country: 'USA',
        price: 1000000,
      },
    ]);
  };

  return (
    <>
      <div className="row mb-2">
        <h5 className="themeFontColor text-center">
          Houses currently on the market
        </h5>
      </div>
      <table className="table table-hover">
        <thead>
          <tr>
            <th>Address</th>
            <th>Country</th>
            <th>Asking Price</th>
          </tr>
        </thead>
        <tbody>
          {houses.map((house) => (
            <HouseRow house={house} key={house.id} />
          ))}
        </tbody>
      </table>
      <button onClick={addHouse} className="btn btn-primary">
        Add
      </button>
    </>
  );
};

export default HouseList;
