import { useNavigate } from "react-router-dom";
import config from "../config";
import { House } from "../types/house";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import axios, { AxiosError, AxiosResponse } from "axios";
import Problem from "../types/problem";

function useFetchHouses() {
  return useQuery<House[], AxiosError>({
    queryKey: ["houses"],
    queryFn: () =>
      axios.get(`${config.baseApiUrl}/houses`).then((res) => res.data),
  });
}

function useFetchHouse(id: number) {
  return useQuery<House, AxiosError>({
    queryKey: ["houses", id],
    queryFn: () =>
      axios.get(`${config.baseApiUrl}/house/${id}`).then((res) => res.data),
  });
}

function useAddHouse() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError<Problem>, House>({
    mutationFn: (house) => axios.post(`${config.baseApiUrl}/houses`, house),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["houses"] });
      navigate("/");
    },
  });
}

function useUpdateHouse() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError<Problem>, House>({
    mutationFn: (house) => axios.put(`${config.baseApiUrl}/houses`, house),
    onSuccess: (_, house) => {
      queryClient.invalidateQueries({ queryKey: ["houses"] });
      navigate(`/house/${house.id}`);
    },
  });
}

function useDeleteHouse() {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError, House>({
    mutationFn: (house) =>
      axios.delete(`${config.baseApiUrl}/houses/${house.id}`),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["houses"] });
      navigate("/");
    },
  });
}

export default useFetchHouses;
export { useFetchHouse, useAddHouse, useUpdateHouse, useDeleteHouse };
